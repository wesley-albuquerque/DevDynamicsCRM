if (typeof (Wesley) == "undefined") { Wesley = {} }
if (typeof (Wesley.Exercicios) == "undefined") { Wesley.Exercicios = {} }

Wesley.Exercicios = {
    OnClickClon: function (contexto) {
        var oppId;
        if (contexto.data != null) {
            oppId = contexto.data.entity.getId();
        }
        else if (contexto.length > 1) {
            Wesley.Exercicios.DynamicsAlert("Selecione apenas um registro", "Seleção inválida");

        }
        else {
            oppId = contexto[0];
        }
        //var formContext = executionContext;
        //var par2 = parametro2;
        //var par3 = parametro3;
        //var oppId = formContext.data.entity.getId();

        var execute_wes_ClonarProposta_Request = {
            // Parameters
            oppId: oppId, // Edm.String

            getMetadata: function () {
                return {
                    boundParameter: null,
                    parameterTypes: {
                        oppId: { typeName: "Edm.String", structuralProperty: 1 }
                    },
                    operationType: 0, operationName: "wes_ClonarProposta"
                };
            }
        };

        Xrm.WebApi.execute(execute_wes_ClonarProposta_Request).then(
            function success(response) {
                if (response.ok) {
                    return response.json();
                }
            }
        ).then(function (responseBody) {
            var result = responseBody;
            console.log(result);
            Wesley.Exercicios.NavigateNewOpp(result["newOppId"]);

        }).catch(function (error) {
            console.log(error.message);
            if (error.message == "Não existe Cotação na Oportunidade selecionada. Crie a cotação antes de cloná-la.") {
                Wesley.Exercicios.DynamicsAlert(error.message, "Cotação inválida");
            }

        });
    },
    NavigateNewOpp: function (newOppId) {
        var entityformOption = {};
        entityformOption.entityName = "opportunity";
        entityformOption.entityId = newOppId;
        entityformOption.openInNewWindow = true;

        Xrm.Navigation.openForm(entityformOption).then(
            function (success) {
                console.log(true);

            },
            function (error) {
                console.log(error)
            }
        )
    },
    DynamicsAlert: function (alertText, alertTitle) {
        var alertStrings = {
            confirmButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };
        var alertOptions = {
            height: 220,
            width: 220
        };
        return Xrm.Navigation.openAlertDialog(alertStrings, alertOptions);
    }

}