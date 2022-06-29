const connection = new signalR.HubConnectionBuilder()
    .withUrl("/footballersHub")
    .build();

async function start() {
    try {
        await connection.start();
        alert("SignalR Connected.");
        try {
            connection.invoke("JoinGroup", "footballers");
        }
        catch (err) {
            alert(err);
        };
    } catch (err) {
        alert(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

start();

connection.on("UpdateFootballerTable", (footballer) => {
    alert("Update table");
    var t = document.getElementById('tableBody');
    t.insertRow(0).innerHTML = '<tr><td>' + footballer.name + '</td><td>' + footballer.surname + '</td><td>' + footballer.sex + '</td><td>' + footballer.birthDate + '</td><td>' + footballer.teamName + '</td><td>' + footballer.country + '</td><td>' + '<a href=/Page/Edit/' + footballer.id + '>Edit</a>' + '</td></tr>';
});










