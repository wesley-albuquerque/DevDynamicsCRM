if (typeof (Wesley.Exercicios) == "undefined") { Wesley.Exercicios = {} }
if (typeof (Wesley.Exercicios.Conta) == "undefined") { Wesley.Exercicios.Conta = {} }

Wesley.Exercicios.Conta = {
    OnChangeCNPJ: function (executionContext) {
        var formContext = executionContext.getFormContext();
        var cnpj = formContext.getAttribute("wes_cnpj").getValue();
        if (cnpj != null && cnpj != "") {
            cnpj = cnpj.replace(/\D/g, "");
            if (cnpj.length == 14 && this.ValidaCNPJ(cnpj)) {
                cnpj = cnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
                formContext.getAttribute("wes_cnpj").setValue(cnpj);
            }
            else {
                this.DynamicsAlert("CNPJ inexistente", "CNPJ inválido").then(function close() {
                    formContext.getControl("wes_cnpj").setFocus();
                });
            }
        }
    },

    ValidaCNPJ: function (cnpj) {
        var rep = 0;
        for (var g = 1; g > 14; g++) {
            if (cnpj[0] == cnpj[g])
                rep++
        }
        if (rep == 13) {
            return false
        }

        let soma = 0;
        for (let i = 0; i < 12; i++) {
            soma += parseInt(cnpj.charAt(i)) * (i < 4 ? 5 - i : 13 - i);
        }
        let digito1 = (11 - soma % 11) % 10;

        soma = 0;
        for (let i = 0; i < 13; i++) {
            soma += parseInt(cnpj.charAt(i)) * (i < 5 ? 6 - i : 14 - i);
        }
        let digito2 = (11 - soma % 11) % 10;

        return (parseInt(cnpj.charAt(12)) === digito1 && parseInt(cnpj.charAt(13)) === digito2);
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
