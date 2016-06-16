app.factory('GateService', ['$http', '$q', function ($http, $q) {
    var GateService = {};
    var gates = [];
    GateService.getItem = function (url) {
        var deferred = $q.defer();
        $http.get(url).success(function (data) {
            deferred.resolve(data);
        });
        return deferred.promise;
    }
    GateService.addItem = function (url, parms) {
        return $http.post(url, parms);
    }
    GateService.updateItem = function (url, parms) {
        return $http.post(url, parms);
    }
    GateService.removeItem = function (item) { gates.splice(list.indexOf(item), 1) }
    GateService.size = function () { return gates.length; }
    GateService.hours = function () {
        var hours = [];
        for (var i = 0; i <= 23; i++) {
            if (i < 10) hours.push('0'+ i);
            else hours.push(i);
        }
        return hours;
    }
    GateService.mins = function () {
        var mins = [];
        for (var i = 0; i <= 59; i++) {
            if (i < 10) mins.push('0' + i);
            else mins.push(i);
        }
        return mins;
    }
    return GateService;
}]);
