if (typeof (Wesley) == "undefined") { Wesley = {} }
if (typeof (Wesley.Exercicios) == "undefined") { Wesley.Exercicios = {} }

Wesley.Exercicios = {
    OnChangeCEP: function (executionContext) {
        var formContext = executionContext.getFormContext();
        var cep = formContext.getAttribute("address1_postalcode").getValue();

        if (cep != null && cep != "") {
            cep = cep.replace(/\D/g, "")

            if (cep.length == 8) {
                var execute_wes_BuscaCEP_Request = {
                    CEP: cep,

                    getMetadata: function () {
                        return {
                            boundParameter: null,
                            parameterTypes: {
                                CEP: { typeName: "Edm.String", structuralProperty: 1 }
                            },
                            operationType: 0, operationName: "wes_BuscaCEP"
                        };
                    }
                };

                Xrm.WebApi.execute(execute_wes_BuscaCEP_Request).then(
                    function success(response) {
                        if (response.ok) { return response.json(); }
                    }
                ).then(function (responseBody) {
                    var result = responseBody;
                    console.log(result);
                   
                    var logradouro = result["logradouro"]; 
                    var complemento = result["complemento"]; 
                    var bairro = result["bairro"]; 
                    var localidade = result["localidade"]; 
                    var uf = result["uf"]; 
                    var ibge = result["ibge"]; 
                    var ddd = result["ddd"]; 
                    var erro = result["erro"]; 
                    cep = cep.replace(/(\d{5})(\d{3})/, "$1-$2");

                    formContext.getAttribute("address1_postalcode").setValue(cep);
                    formContext.getAttribute("address1_line1").setValue(logradouro);
                    formContext.getAttribute("address1_line2").setValue(complemento);
                    formContext.getAttribute("address1_postofficebox").setValue(bairro);
                    formContext.getAttribute("address1_city").setValue(localidade);
                    formContext.getAttribute("address1_stateorprovince").setValue(uf);
                    formContext.getAttribute("wes_ibge").setValue(ibge);
                    formContext.getAttribute("wes_ddd").setValue(ddd);
                    formContext.getAttribute("address1_country").setValue("Brasil");

                    if (erro) {
                        formContext.getAttribute("address1_country").setValue("");
                        Wesley.Exercicios.DynamicsAlert("CEP inexistente", "CEP inválido").then(function close() {
                            formContext.getControl("address1_postalcode").setFocus();

                        });
                    }

                }).catch(function (error) {
                    console.log(error.message);
                });


            }
            else {
                Wesley.Exercicios.DynamicsAlert("Informe um CEP", "CEP inválido").then(function close() {
                    formContext.getControl("address1_postalcode").setFocus();
                });
                formContext.getAttribute("address1_line1").setValue("");
                formContext.getAttribute("address1_line2").setValue("");
                formContext.getAttribute("address1_postofficebox").setValue("");
                formContext.getAttribute("address1_city").setValue("");
                formContext.getAttribute("address1_stateorprovince").setValue("");
                formContext.getAttribute("wes_ibge").setValue("");
                formContext.getAttribute("wes_ddd").setValue("");
                formContext.getAttribute("address1_country").setValue("");
            }
        }
        else
            return


    },
    DynamicsAlert: function (alertText, alertTitle) {
        var alertStrings = {
            confirmButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };
        var alertOptions = {
            height: 120,
            width: 200
        };
        return Xrm.Navigation.openAlertDialog(alertStrings, alertOptions);
    }

}