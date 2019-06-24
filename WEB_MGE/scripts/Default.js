function GerarGraficos(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10) {
    var options = { responsive: true };

    var vInsTot = 20;
    var vInsTotAmostra = 50;

    var dataInstalados = [{ value: v1, color: "#FFB100", highlight: "#CC8E00", label: "Instalados" },
                             { value: v2, color: "#E2E2E2", highlight: "#C3C0C0", label: "Pendente" }]

    var dataLeituras = [{ value: v3, color: "#FFB100", highlight: "#CC8E00", label: "Leituras" },
                             { value: v4, color: "#E2E2E2", highlight: "#C3C0C0", label: "Pendente" }]

    var dataRetirados = [{ value: v5, color: "#FFB100", highlight: "#CC8E00", label: "Retirados" },
                             { value: v6, color: "#E2E2E2", highlight: "#C3C0C0", label: "Pendente" }]

    var dataDepuracao = [{ value: v7, color: "#FFB100", highlight: "#CC8E00", label: "Depurados" },
                             { value: v8, color: "#E2E2E2", highlight: "#C3C0C0", label: "Pendente" }]

    var dataEnviados = [{ value: v9, color: "#FFB100", highlight: "#CC8E00", label: "Enviados" },
                             { value: v10, color: "#E2E2E2", highlight: "#C3C0C0", label: "Pendente" }]


    var ctx = document.getElementById("GraficoInstalacoes").getContext("2d");
    var PizzaChart = new Chart(ctx).Doughnut(dataInstalados, options);

    var ctx = document.getElementById("GraficoLeituras").getContext("2d");
    var PizzaChart = new Chart(ctx).Doughnut(dataLeituras, options);

    var ctx = document.getElementById("GraficoRetirados").getContext("2d");
    var PizzaChart = new Chart(ctx).Doughnut(dataRetirados, options);

    var ctx = document.getElementById("GraficoDepuracao").getContext("2d");
    var PizzaChart = new Chart(ctx).Doughnut(dataDepuracao, options);

    var ctx = document.getElementById("GraficoEnviados").getContext("2d");
    var PizzaChart = new Chart(ctx).Doughnut(dataEnviados, options);
}