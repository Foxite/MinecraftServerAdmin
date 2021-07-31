async function getBanList() {
    let result = await apiCall("GET", "Whitelist");
    return JSON.parse(result);
}

function addToWhitelist(player) {
    return apiCall("PUT", "Whitelist/" + player);
}

function removeFromWhitelist(player) {
    return apiCall("DELETE", "Whitelist/" + player);
}