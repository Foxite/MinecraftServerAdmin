const API_BASE = "http://localhost:5000/";
//const API_BASE = "https://admin.corsac.nl/ik_ihe/"

// https://stackoverflow.com/a/48969580/3141917
function makeRequest(method, url, data) {
    return new Promise(function (resolve, reject) {
        let xhr = new XMLHttpRequest();
        xhr.open(method, url);
        xhr.onload = function () {
            if (this.status >= 200 && this.status < 300) {
                resolve(xhr.response);
            } else {
                reject({
                    status: this.status,
                    statusText: xhr.statusText
                });
            }
        };
        xhr.onerror = function () {
            reject({
                status: this.status,
                statusText: xhr.statusText
            });
        };
        if (data !== undefined) {
            xhr.setRequestHeader("Content-type", "application/json");
            xhr.send(JSON.stringify(data));
        } else {
            xhr.send();
        }
    });
}

function apiCall(method, endpoint, data) {
    return makeRequest(method, API_BASE + endpoint, data); 
}

function executeCommand(command) {
    return apiCall("POST", "Command", command);
}

async function updatePlayerList(getList, listElement) {
    listElement.innerHTML = "";

    for (let playerName of await getList()) {
        const option = document.createElement("option");
        option.innerText = playerName;
        option.id = "playerlist_item_" + playerName;
        listElement.add(option);
    }
}
