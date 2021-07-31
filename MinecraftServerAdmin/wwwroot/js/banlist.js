async function getBanlist() {
    let result = await apiCall("GET", "Ban");
    return JSON.parse(result);
}

function banPlayer(player, reason) {
    return apiCall("PUT", "Ban/Player/" + player, reason);
}

function unbanPlayer(player) {
    return apiCall("DELETE", "Ban/Player/" + player);
}

function banIp(ip, reason) {
    return apiCall("PUT", "Ban/Ip/" + ip, reason);
}

function unbanIp(ip) {
    return apiCall("DELETE", "Ban/Ip/" + ip);
}

function updateBanlist() {
    return updatePlayerList(getBanlist, document.getElementById("banlist"));
}

function inputBanlist(element) {
    element.value = element.value; // to make sure only one is selected
    document.getElementById("ban_player_name").value = element.value
}

async function btnBanUser(event) {
    const target = document.getElementById("ban_player_name").value;
    const reason = document.getElementById("ban_reason").value;
    if (target.indexOf('.') !== -1) {
        await banIp(target, reason)
    } else {
        await banPlayer(target, reason)
    }
}

async function btnUnbanUser(event) {
    const target = document.getElementById("ban_player_name").value;
    if (target.indexOf('.') !== -1) {
        await unbanIp(target)
    } else {
        await unbanPlayer(target)
    }
}
