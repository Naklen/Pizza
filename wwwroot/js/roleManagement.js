let select = document.querySelector('.role-management-panel__select');
select.addEventListener('change', async (e) => {
    let url = await fetch(`/Admin/GetUserList?Sort=${sortMap[e.target.value]}`, {
        method: 'GET'        
    });
    let res = await (await fetch(url.url)).body.getReader().read();
    let str = new TextDecoder("utf-8").decode(res.value);
    document.querySelector('.role-management-panel__user-list').innerHTML = str;
});

let adminCheckboxes = document.getElementsByClassName('role-management__admin-checkbox');
for (let ac of adminCheckboxes) {
    ac.addEventListener('click', async (e) => {
        await fetch('/Admin/ChangeUserRoles', {
            method: 'POST',
            body: JSON.stringify({ UserName: e.target.name, RoleName: "admin", HasRole: e.target.checked }),
            headers: {
                'Content-Type': 'application/json'
            }
        });
    });
}

let managerCheckboxes = document.getElementsByClassName('role-management__manager-checkbox');
for (let mc of managerCheckboxes) {
    mc.addEventListener('click', async (e) => {
        await fetch('/Admin/ChangeUserRoles', {
            method: 'POST',
            body: JSON.stringify({ UserName: e.target.name, RoleName: "manager", HasRole: e.target.checked }),
            headers: {
                'Content-Type': 'application/json'
            }
        });
    });
}

const sortMap = {
    "all": 0,
    "admin": 1,
    "manager": 2,
    "user": 3
}