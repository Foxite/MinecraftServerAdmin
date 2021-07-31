async function getBanList() {
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