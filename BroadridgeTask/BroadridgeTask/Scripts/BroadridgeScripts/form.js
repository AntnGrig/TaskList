'use strict';

(function () {
    var ctrlName = 'SPA';
    var procLoadName = 'RunLoadJson';
    var procSaveName = 'RunSaveJson';
    var procDeleteName = 'RunDeleteJson';
    var activeStatus = 'Active';
    var completedStatus = 'Completed';
    var removeCaption = 'Remove';
    var completeCaption = 'Complete';
    var defaultDataFields = {
        'TaskStatus': activeStatus,
        'dayToComplete': 0,
        'hourToComplete': 0,
        'minuteToComplete': 0,
        'secToComplete': 0
    };
    var taskListPageName = 'TaskList';
    var addTaskPageName = 'AddTask';
    var fltrEq = 'equals';
    var ID = 'ID';
    var curFilter = '';
   
    broadridge_module.controller("ctrl", function ($scope, $controller, $interval, $http, $timeout) {
        $scope.datatable = [];
        $scope.rowData = {};
        $scope.fltrData = [];
        $scope.dataFields = broadridge_task.fn.getObjPrimCopy(defaultDataFields);
        $scope.pageName = taskListPageName;
        $scope.validation = { vldtnShow: false, vldtnErrMsgs: [] };
        $scope.inProgress = 0;

        angular.extend(this, $controller('ctrl-shared', {
            $scope: $scope
        }));
        
        angular.element(document).ready(function () {
            $scope.listLoad();
        });

        $interval(countDownDate, 500);

        var columnDefs = [
            { headerName: "Name", field: "Name", width: 250, cellRenderer: cellRendFunc0 },
            { headerName: "Priority", field: "TaskPriority", width: 100, cellRenderer: cellRendFunc1 },
            { headerName: "Added", field: "Added", width: 150, cellRenderer: cellRendFunc2 },
            { headerName: "Time to complete", field: "DateToComplete", width: 150, cellRenderer: cellRendFunc3 },
            { headerName: "Actions", field: "TaskStatus", width: 100, cellRenderer: cellRendFunc4 },
            { headerName: "", field: "", width: 100, cellRenderer: cellRendFunc5 }
        ];

        $scope.gridOptions = {
            columnDefs: columnDefs,
            rowData: null,
            angularCompileRows: true,
            enableColResize: true,
            enableSorting: true,
            enableFilter: true
        };

        $scope.activeFltr = function () {
            curFilter = activeStatus;
            var countryFilterComponent = $scope.gridOptions.api.getFilterInstance('TaskStatus');
            countryFilterComponent.filter = fltrEq;
            countryFilterComponent.filterCondition = fltrEq;
            countryFilterComponent.filterText = activeStatus.toLowerCase();
            $scope.gridOptions.api.onFilterChanged();
        };

        $scope.completedFltr = function () {
            curFilter = completedStatus;
            var countryFilterComponent = $scope.gridOptions.api.getFilterInstance('TaskStatus');
            countryFilterComponent.filter = fltrEq;
            countryFilterComponent.filterCondition = fltrEq;
            countryFilterComponent.filterText = completedStatus.toLowerCase();
            $scope.gridOptions.api.onFilterChanged();
        };

        $scope.clearFltr = function () {
            curFilter = '';
            $scope.gridOptions.api.setFilterModel(null);
            $scope.gridOptions.api.onFilterChanged();
        };

        $scope.btnCellClicked = function (trgtObj) {
            if (trgtObj.TaskStatus === completedStatus) {
                $scope.taskDelete(trgtObj);
            }
            if (trgtObj.TaskStatus === activeStatus) {
                $scope.taskUpdate(trgtObj);
            }
        };

        function cellRendFunc0(params) {
            return '<div ng-click="cellClicked(\'Name\', data.Name)" ng-bind="data.Name"></div>';
        }

        function cellRendFunc1(params) {
            return '<div ng-click="cellClicked(\'Name\', data.Name)" ng-bind="data.TaskPriority"></div>';
        }

        function cellRendFunc2(params) {
            return '<div ng-click="cellClicked(\'Name\', data.Name)" ng-bind="data.AddedFormat"></div>';
        }

        function cellRendFunc3(params) {
            return '<div ng-click="cellClicked(\'Name\', data.Name)" ng-bind="data.TimeToComplete"></div>';
        }

        function cellRendFunc4(params) {
            return '<div ng-click="cellClicked(\'Name\', data.Name)" ng-bind="data.TaskStatus"></div>';
        }

        function cellRendFunc5(params) {
            return '<button class="btn btn-danger btn-xs btn-task-grid" ng-click="btnCellClicked(data)" ng-bind="setBtnCaption(data.TaskStatus)"></button>';
        }

        $scope.cellClicked = function (key, value) {
            for (var i = 0; i < $scope.dataTable.length; i++) {
                if ($scope.dataTable[i][key] === value) {
                    $scope.rowData = $scope.dataTable[i];
                    break;
                }
            }
        };

        $scope.listLoad = function () {
            var type, data;
            var urlTarget;
            $scope.manageProgress(true);
            urlTarget = broadridge_task.fn.getCtrlURL(ctrlName, procLoadName);
            $scope.vldtnReset();

            $http({
                method: 'post',
                url: urlTarget
            }).then(function (response) {

                type = response.data.sourceType;
                data = response.data.jsonData;

                if (type === broadridge_task.vr.sourceTypeData) {
                    if (data instanceof Array) {
                        for (var i = 0; i < data.length; i++) {
                            data[i]['AddedFormat'] = broadridge_task.fn.ticksToDateString(data[i]['Added']);
                            data[i]['Added'] = broadridge_task.fn.ticksToDate(data[i]['Added']);
                            data[i]['DateToComplete'] = broadridge_task.fn.ticksToDate(data[i]['DateToComplete']);
                        }
                        $scope.dataTable = data;
                        $scope.gridOptions.api.setRowData($scope.dataTable);
                    }
                }
                $scope.vldtnDataHandler(type, data);
                $scope.manageProgress(false);
            }, function (error) {
                $scope.errorHandler(error.statusText);
                $scope.manageProgress(false);
            });
        };

        $scope.openAddTaskPage = function () {
            $scope.vldtnReset();
            $scope.pageName = addTaskPageName;
            $scope.dataFields = broadridge_task.fn.getObjPrimCopy(defaultDataFields);
        };

        $scope.openTaskListPage = function (pageName) {
            $scope.vldtnReset();
            $scope.pageName = taskListPageName;
            $timeout(function () {
                $scope.listLoad();
            });
        };

        $scope.setBtnCaption = function (val) {
            let caption;
            caption = val === activeStatus ? completeCaption : '';
            caption = val === completedStatus ? removeCaption : caption;
            return caption;
        };

        $scope.taskAdd = function () {
            var type, data;
            var urlTarget, dataObj;
            $scope.manageProgress(true);
            $scope.vldtnReset();
            urlTarget = broadridge_task.fn.getCtrlURL(ctrlName, procSaveName);
            dataObj = $scope.dataFields;
            dataObj.DateToComplete = broadridge_task.fn.getDateByDHMS($scope.dataFields.dayToComplete, $scope.dataFields.hourToComplete, $scope.dataFields.minuteToComplete, $scope.dataFields.secToComplete);
            $http({
                method: 'post',
                url: urlTarget,
                data: JSON.stringify({ 'model': dataObj })
            }).then(function (response) {

                type = response.data.sourceType;
                data = response.data.jsonData;

                $scope.vldtnDataHandler(type, data);
                $scope.manageProgress(false);
            }, function (error) {
                $scope.errorHandler(error.statusText);
                $scope.manageProgress(false);
            });
        };

        $scope.taskDelete = function (trgtRow) {
            var type, data;
            var dataObj, urlTarget, index;
            $scope.manageProgress(true);
            $scope.vldtnReset();
            urlTarget = broadridge_task.fn.getCtrlURL(ctrlName, procDeleteName);
            dataObj = { ID: trgtRow.ID };
            $http({
                method: 'post',
                url: urlTarget,
                data: JSON.stringify({ 'model': dataObj })
            }).then(function (response) {

                type = response.data.sourceType;
                data = response.data.jsonData;
                $scope.manageProgress(false);

                if (type === broadridge_task.vr.sourceTypeData) {
                    if (trgtRow.ID === $scope.rowData.ID) { $scope.rowData = {}; }
                    broadridge_task.fn.objArrDelByKeyVal($scope.dataTable, ID, trgtRow.ID);
                    $scope.gridOptions.api.setRowData($scope.dataTable);
                    setFilter();
                }

                $scope.vldtnDataHandler(type, data);
                $scope.manageProgress(false);
            }, function (error) {
                $scope.errorHandler(error.statusText);
                $scope.manageProgress(false);
            });
        };

        $scope.taskUpdate = function (trgtRow) {
            var type, data;
            var urlTarget, dataObj;
            $scope.manageProgress(true);
            $scope.vldtnReset();
            urlTarget = broadridge_task.fn.getCtrlURL(ctrlName, procSaveName);
            dataObj = broadridge_task.fn.getObjPrimCopy(trgtRow);
            dataObj.TaskStatus = completedStatus;
            $http({
                method: 'post',
                url: urlTarget,
                data: JSON.stringify({ 'model': dataObj })
            }).then(function (response) {

                type = response.data.sourceType;
                data = response.data.jsonData;

                if (type === broadridge_task.vr.sourceTypeData) {
                    trgtRow.TaskStatus = completedStatus;
                    setFilter();
                }

                $scope.vldtnDataHandler(type, data);
                $scope.manageProgress(false);
            }, function (error) {
                $scope.errorHandler(error.statusText);
                $scope.manageProgress(false);
            });
        };

        function countDownDate() {
            if ($scope.pageName === taskListPageName &&
                $scope.dataTable &&
                $scope.dataTable.length !== 0) {
                for (var i = 0; i < $scope.dataTable.length; i++) {
                    if ($scope.dataTable[i].DateToComplete instanceof Date) {
                        var now = new Date().getTime();
                        var countDownDate = $scope.dataTable[i].DateToComplete.getTime();
                        var distance = countDownDate - now;

                        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
                        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

                        $scope.dataTable[i].TimeToComplete = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";

                        if (distance < 0) {
                            $scope.dataTable[i].TimeToComplete = "Expired";
                        }
                    }
                }
            }
        }

        function setFilter() {
            switch(curFilter) {
                case activeStatus:
                    $scope.activeFltr();
                    break;
                case completedStatus:
                    $scope.completedFltr();
                    break;
                default:
                    $scope.clearFltr();
                    break;
            }
        }
    });
})();