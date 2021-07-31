async function getWhitelist() {
    let result = await apiCall("GET", "Whitelist");
    return JSON.parse(result);
}

function addToWhitelist(player) {
    return apiCall("PUT", "Whitelist/" + player);
}

function removeFromWhitelist(player) {
    return apiCall("DELETE", "Whitelist/" + player);
}

function updateWhitelist() {
    return updatePlayerList(getWhitelist, document.getElementById("whitelist"));
}

function inputWhitelist(element) {
    element.value = element.value; // to make sure only one is selected
    document.getElementById("whitelist_player_name").value = element.value
}

async function btnWhitelistUser(event) {
    const target = document.getElementById("whitelist_player_name").value;
    await addToWhitelist(target)
}

async function btnUnwhitelistUser(event) {
    const target = document.getElementById("whitelist_player_name").value;
    await removeFromWhitelist(target)
}
