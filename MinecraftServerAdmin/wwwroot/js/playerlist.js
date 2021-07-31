async function getPlayerList() {
    let result = await apiCall("GET", "Players");
    return JSON.parse(result);
}

setInterval(async () => {
    const playerList = document.getElementById("playerlist");
    playerList.options.clear();
    
    for (let playerName in playerList) {
        const option = document.createElement("option");
        option.innerText = playerName;
        option.id = "playerlist_item_" + playerName;
        playerList.add(option);
    }
}, 5000);