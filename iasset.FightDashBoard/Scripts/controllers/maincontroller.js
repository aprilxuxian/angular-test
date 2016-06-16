var erorMsg = 'Ops, the service is temporarily unavaiable, please contact the system adminstrator.';
app.controller('LandingPageController', ['$scope', '$http', 'GateService', function LandingPageController($scope, $http, GateService) {
    $scope.loading = true;
    GateService.getItem('/gates').then(function (data) {
        if (!data.gatesModels) {
            alert(data.data.error);
        } else {
            $scope.models = {
                title: 'Flight Dashboard',
                gates: data.gatesModels,
                gatequeryresult: data.gatesModels
            };
            $scope.loading = false;
        }
    }, function (result) {
        alert(erorMsg);
    });
    $scope.cancelFlight = function (gateNumber, flightNumber) {
        var flight =
        {
           FlightNumber: flightNumber
        }
        GateService.updateItem('gate/' + gateNumber + '/cancelflight', flight).then(function (result) {
            if (result.data &&result.data.error) {
                alert(result.data.error);
            } else {
                $scope.selectGate(gateNumber);
            }
        }, function(result, textStatus, errorThrown) {
            alert(erorMsg);
        });
        $scope.loading = false;
    };
    $scope.changeGate = function (gateNumber, flightNumber) {
        if (!$scope.selectedChangeGate) {
            alert("Please select a gate to assign!");
        }
        var flight =
        {
            FlightNumber: flightNumber
        }
        GateService.updateItem('fight/' + gateNumber + '/changegate/' + $scope.selectedChangeGate, flight).then(function (result) {
            if (result.data && result.data.error) {
                alert(result.data.error);
            } else {
                $scope.selectGate($scope.selectedChangeGate);
            }
        }, function (result, textStatus, errorThrown) {
            alert(erorMsg);
        });
        $scope.loading = false;
    };
    $scope.selectGate = function (selectedGate) {
        GateService.getItem('/' + selectedGate + '/flights').then(function (result) {
            if (result.data &&result.data.error) {
                alert(result.data.error);
            } else {
                $scope.models.gatequeryresult = result;
            }
        }, function (result, textStatus, errorThrown) {
            alert(erorMsg);
        });
        $scope.loading = false;
    };
    $scope.selectChangeGate = function (selectedChangeGate) {
        $scope.selectedChangeGate = selectedChangeGate;
    };
}]);
app.controller('AddFlightController', ['$scope', '$routeParams', 'GateService', '$window', function AddFlightController($scope, $routeParams, GateService, $window) {
    $scope.loading = true;
    $scope.gateid = $routeParams.gateid;
    $scope.title = 'Add Flight';
    $scope.hours = GateService.hours();
    $scope.mins = GateService.mins();
    $scope.submit = function () {
        var flight =
        {
            ArrivalTime: $scope.selectedArrivalHour + ':' + $scope.selectedArrivalMin,
            DepartureTime: $scope.selectedDepartureHour + ':' + $scope.selectedDepartureMin,
            FlightNumber: $scope.FlightNumber
        }
        GateService.addItem('/gate/' + $scope.gateid, flight).then(function (result) {
            if (result.data.error) {
                alert(result.data.error);
            } else {
                $window.location.href = "/";
            }
        }, function(result) {
            alert(erorMsg);
        });
    };
    $scope.loading = false;
}]);
app.controller('UpdateFlightController', ['$scope', '$routeParams', 'GateService', '$window', function UpdateFlightController($scope, $routeParams, GateService, $window) {
    $scope.loading = true;
    $scope.gateid = $routeParams.gateid;
    $scope.flightno = $routeParams.flightno;
    $scope.title = 'Update Flight';
    $scope.hours = GateService.hours();
    $scope.mins = GateService.mins();
    GateService.getItem('flights/' + $scope.flightno).then(function (data) {
        if (data.data && data.data.error) {
            alert(data.data.error);
        } else {
            var flight = data.flight;
            if (flight.ArrivalTime) {
                var arrival = flight.ArrivalTime.split(":");
                $scope.selectedArrivalHour = arrival[0].trim();
                $scope.selectedArrivalMin = arrival[1].trim();
            }
            if (flight.DepartureTime) {
                var depart = flight.DepartureTime.split(":");
                $scope.selectedDepartureHour = depart[0].trim();
                $scope.selectedDepartureMin = depart[1].trim();
            }
            $scope.loading = false;
        }
    }, function (result) {
        alert(erorMsg);
    });
    $scope.submit = function () {
        var flight =
        {
            ArrivalTime: $scope.selectedArrivalHour + ':' + $scope.selectedArrivalMin,
            DepartureTime: $scope.selectedDepartureHour + ':' + $scope.selectedDepartureMin,
            FlightNumber: $scope.flightno
        }
        GateService.updateItem('/gate/' + $scope.gateid + '/updateflight', flight).then(function (result) {
            if (result.data.error) {
                alert(result.data.error);
            } else {
                $window.location.href = "/";
            }
        }, function (result) {
            alert(result.error);
        });
    };
}]);
