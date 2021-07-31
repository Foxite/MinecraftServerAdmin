async function getOnlinePlayerList() {
    let result = await apiCall("GET", "Players");
    return JSON.parse(result);
}

function updateOnlinePlayerList() {
    return updatePlayerList(getOnlinePlayerList, document.getElementById("playerlist"));
}