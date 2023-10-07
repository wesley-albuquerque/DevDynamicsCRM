if (typeof (Exercicios) == "undefined") { Exercicios = {} }
if (typeof (Exercicios.Oportunidade) == "undefined") { Exercicios.Oportunidade = {} }

Exercicios.Oportunidade = {
    Onload: function (executionContext) {
        var formContext = executionContext.getFormContext();
        formContext.ui.setFormNotification("Este � integrado e n�o pode ser editado", "WARNING");
        var integracao = formContext.getAttribute("wes_integracao").getValue();
        if (integracao) {
            var campos = formContext.getControl();
            campos.forEach(function (atributo) {
                atributo.setDisabled(true)
            });
        }

    }

}


