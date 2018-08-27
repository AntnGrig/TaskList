'use strict';
var broadridge_task = {};
broadridge_task.vr = {};

broadridge_task.fn = {
    objArrDelByKeyVal: function (source, key, value) {
        if (source instanceof Array) {
            for (var i = 0; i < source.length; i++) {
                if (source[i][key] === value) {
                    source.splice(i, 1);
                    break;
                }
            }
        }
    },
    ticksToDateString: function (value) {
        var pattern = /Date\((\d+)\)/;
        var ticks = pattern.exec(value);

        if (ticks != null) {
            var date = new Date(parseFloat(ticks[1]));
            //var d = date.getDate();
            //var m = date.getMonth() + 1;
            //var y = date.getFullYear();
            var h = date.getHours();
            var min = date.getMinutes();
            //return (d > 9 ? '' : '0') + d + "-" + (m > 9 ? '' : '0') + m + "-" + y;
            return date.toLocaleDateString() + ' ' + (h > 9 ? '' : '0') + h + ":" + (min > 9 ? '' : '0') + min;
        }
        else {
            return value
        }
    },
    ticksToDate: function (value) {
        var pattern = /Date\((\d+)\)/;
        var ticks = pattern.exec(value);

        if (ticks != null) {
            var date = new Date(parseFloat(ticks[1]));
            return date;
        }
        else {
            return value
        }
    },
    getCtrlURL: function (ctrl, method) {
        var backSlash = '\\';
        return backSlash.concat(ctrl, backSlash, method);
    },
    getDateByDHMS: function (days, hours, minutes, seconds) {
        var now, delta, result;
        if (days === parseInt(days, 10) &&
            hours === parseInt(hours, 10) &&
            minutes === parseInt(minutes, 10) &&
            seconds === parseInt(seconds, 10)) {
            now = new Date().getTime();
            delta = 86400000 * days + hours * 3600000 + minutes * 60000 + seconds * 1000;
            result = new Date(now + delta);
        }
        return result;
    },
    getObjPrimCopy: function (trgtObj) {
        return JSON.parse(JSON.stringify(trgtObj)); // Primitives only
    },
    getTicks: function (value) {
        var result;
        var pattern = /Date\((\d+)\)/;
        var ticks = pattern.exec(value);
        if (ticks != null) {
            result = parseFloat(ticks[1]);
        }
        return result;
    }
};

broadridge_task.vr.sourceTypeData = 'data';
broadridge_task.vr.sourceTypeValidation = 'validation';