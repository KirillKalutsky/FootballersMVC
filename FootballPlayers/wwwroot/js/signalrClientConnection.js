const connection = new signalR.HubConnectionBuilder()
    .withUrl("/footballersHub")
    .build();

async function start() {
    try {
        await connection.start();
        alert("SignalR Connected.");
    } catch (err) {
        alert(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

start();










