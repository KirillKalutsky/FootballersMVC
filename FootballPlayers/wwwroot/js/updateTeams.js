const connection = new signalR.HubConnectionBuilder()
    .withUrl("/footballersHub")
    .build();

async function start() {
    try {
        await connection.start();
        alert("SignalR Connected.");
        try {
            connection.invoke("JoinGroup", "teams");
            alert("Success join");
        }
        catch(err) {
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

connection.on("UpdateTeamsList", (team) => {
    var newOption = document.createElement('option');
    newOption.innerText = team.name;
    newOption.setAttribute('value', team.id);

    var categories = document.getElementById("teamList");

    categories.appendChild(newOption);
});