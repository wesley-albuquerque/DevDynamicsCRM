if (typeof (Wesley.Exercicios) == "undefined") { Wesley.Exercicios = {} }
if (typeof (Wesley.Exercicios.Conta) == "undefined") { Wesley.Exercicios.Conta = {} }

Wesley.Exercicios.Conta = {
    OnChangeName: function (executionContext) {
        var formContext = executionContext.getFormContext();
        var name = formContext.getAttribute("name").getValue();
        if (name != null && name != ""){
            name = name.toLowerCase();
            name = name.replace(/\s{2,}/g, ' ');
            var palavras = name.split(" ");
            palavras.forEach(function (palavra, indice, array) {
                array[indice] = palavra[0].toUpperCase() + palavra.substring(1)
            });
            formContext.getAttribute("name").setValue(palavras.join(" "));

        }

    }

}
