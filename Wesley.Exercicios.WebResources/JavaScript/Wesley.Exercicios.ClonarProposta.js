if (typeof (Wesley) == "undefined") { Wesley = {} }
if (typeof (Wesley.Exercicios) == "undefined") { Wesley.Exercicios = {} }

Wesley.Exercicios = {
    OnClickClon: function (executionContext) {
        var formContext = executionContext.getFormContext();
        var oppId = formContext.data.entity.getId();

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
                if (response.ok) { console.log("Success"); }
            }
        ).catch(function (error) {
            console.log(error.message);
        });
    }
}