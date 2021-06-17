function GerarGraficos(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10) {
    var options = { responsive: true };

    var vMonTot = 20;
    var vMonTotAmostra = 50;

    var dataMontagem = [{ value: v1, color: "#FFB100", highlight: "#CC8E00", label: "Montagem" },
                             { value: v2, color: "#E2E2E2", highlight: "#C3C0C0", label: "Pendente" }]

    var dataTenAplicada = [{ value: v3, color: "#FFB100", highlight: "#CC8E00", label: "Tensao Aplicada" },
                             { value: v4, color: "#E2E2E2", highlight: "#C3C0C0", label: "Pendente" }]

    var dataExatidao = [{ value: v5, color: "#FFB100", highlight: "#CC8E00", label: "Exatidao" },
                             { value: v6, color: "#E2E2E2", highlight: "#C3C0C0", label: "Pendente" }]

    var dataMarcaLaser = [{ value: v7, color: "#FFB100", highlight: "#CC8E00", label: "Marca Laser" },
                             { value: v8, color: "#E2E2E2", highlight: "#C3C0C0", label: "Pendente" }]

    var dataTesteFinal = [{ value: v9, color: "#FFB100", highlight: "#CC8E00", label: "Teste Final" },
                             { value: v10, color: "#E2E2E2", highlight: "#C3C0C0", label: "Pendente" }]


    var ctx = document.getElementById("GraficoMontagem").getContext("2d");
    var PizzaChart = new Chart(ctx).Doughnut(dataMontagem, options);

    var ctx = document.getElementById("GraficoTenAplicada").getContext("2d");
    var PizzaChart = new Chart(ctx).Doughnut(dataTenAplicada, options);

    var ctx = document.getElementById("GraficoExatidao").getContext("2d");
    var PizzaChart = new Chart(ctx).Doughnut(dataExatidao, options);

    var ctx = document.getElementById("GraficoMarcaLaser").getContext("2d");
    var PizzaChart = new Chart(ctx).Doughnut(dataMarcaLaser, options);

    var ctx = document.getElementById("GraficoTesteFinal").getContext("2d");
    var PizzaChart = new Chart(ctx).Doughnut(dataTesteFinal, options);
}