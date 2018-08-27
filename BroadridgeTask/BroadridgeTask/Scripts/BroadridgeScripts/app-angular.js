'use strict';

agGrid.initialiseAgGridWithAngular1(angular);

var broadridge_module = angular.module("module", ["agGrid"]);

broadridge_module.controller('ctrl-shared', function ($scope) {

    var inProgressClass = 'inProgress';
    var outProgressClass = 'outProgress';
    var vldtnShow = 'vldtnShow';
    var vldtnErrMsgs = 'vldtnErrMsgs';

    $scope.validation = { vldtnShow: false, vldtnErrMsgs: [] };
    $scope.inProgress = 0;

    $scope.errorHandler = function (text) {
        var msdData = [];
        msdData.push({ ErrMsg: text });
        vldtnView(msdData, true);
    };

    $scope.manageProgress = function (val) {
        $scope.inProgress = val ? 1 : 0;
    };

    $scope.showProgress = function () {
        return $scope.inProgress == 1 ? inProgressClass : outProgressClass;
    };

    $scope.vldtnReset = function (type, data) {
        vldtnView();
    };

    $scope.vldtnDataHandler = function (type, data) {
        if (type == broadridge_task.vr.sourceTypeValidation) {
            vldtnView(data, true);
        }
    };

    function vldtnView (msgData, msgShow) {
        msgData = msgData || [];
        msgShow = msgShow || false;
        $scope.validation[vldtnErrMsgs] = msgData;
        $scope.validation[vldtnShow] = msgShow;
    };
});