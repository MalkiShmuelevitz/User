const getDataFromRegister = () => {
    const username = document.querySelector("#username").value;
    const password = document.querySelector("#password2").value;
    const firstname = document.querySelector("#firstname").value;
    const lastname = document.querySelector("#lastname").value;
    return { username, password, firstname, lastname };
};

const getDataFromUpdate = () => {
    const username = document.querySelector("#usernameOnUpdate").value;
    const password = document.querySelector("#password2").value;
    const firstname = document.querySelector("#firstnameOnUpdate").value;
    const lastname = document.querySelector("#lastnameOnUpdate").value;
    const userId = sessionStorage.getItem("id");
    return { userId, username, password, firstname, lastname };
};

const getDataFromLogin = () => {
    const username = document.querySelector("#nameInput").value;
    const password = document.querySelector("#passwordInput").value;
    const firstname = "no-name";
    const lastname = "no-name";
    return { username, password, firstname, lastname };
};

const login = async () => {
    const user = getDataFromLogin();
    try {
        const data = await fetch(`api/Users/login/?username=${user.username}&password=${user.password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (data.status === 204) {
            throw new Error("User not found");
        }
        if (data.status === 400) {
            throw new Error("All fields are required");
        }
        const dataLogin = await data.json();
        sessionStorage.setItem("id", dataLogin.id);
        window.location.href = 'Products.html';
    } catch (error) {
        console.error(error);
        alert(error);
    }
};

const newUser = () => {
    const container = document.querySelector(".container");
    container.classList.remove("container");
};

const closePanel = () => {
    const container = document.querySelector(".container");
    container.classList.add("container");
};

const seeTheUpdateUser = () => {
    const container = document.querySelector(".containerOfUpdate");
    container.classList.remove("containerOfUpdate");
};

const register = async () => {
    const user = getDataFromRegister();
    try {
        if (user.username.length > 50) {
            throw new Error("The username must be smaller than 50 characters");
        }
        if (user.password.length > 20) {
            throw new Error("The password must be smaller than 20 characters");
        }
        if (!user.username || !user.password || !user.firstname || !user.lastname) {
            throw new Error("All fields are required");
        }
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(user.username)) {
            alert("The username must be a valid email address.");
            return;
        }
        const postFromData = await fetch("api/Users", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        if (postFromData.status === 400) {
            throw new Error("All fields are required");
        }
        if (postFromData.status === 422) {
            throw new Error("Password not strong enough");
        }
        if (postFromData.status == 409) {
            throw new Error("Duplicate User");
        }
        const dataPost = await postFromData.json();
        alert("User registered successfully");
    } catch (error) {
        alert(error);
    }
};

const updateUser = async () => {
    const user = getDataFromUpdate();
    try {
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(user.username)) {
            alert("The username must be a valid email address.");
            return;
        }
        if (user.username.length > 50) {
            throw new Error("The username must be smaller than 50 characters");
        }
        if (user.password.length > 20) {
            throw new Error("The password must be smaller than 20 characters");
        }
        if (!user.username || !user.password) {
            throw new Error("Username and password fields are required");
        }
        const updateFromData = await fetch(`api/Users/${sessionStorage.getItem("id")}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        if (updateFromData.status === 400) {
            throw new Error("All fields are required");
        }
        if (updateFromData.status === 404) {
            throw new Error("Password not strong enough");
        }
        alert(`User ${sessionStorage.getItem("id")} updated`);
        window.location.href = 'Products.html';
    } catch (error) {
        alert(error);
    }
};

const checkScore = async () => {
    const password = document.querySelector("#password2").value;
    try {
        const scoreFromData = await fetch("api/Users/password", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(password)
        });
        const scoresDataPost = await scoreFromData.json();
        const score = document.querySelector("#score");
        score.value = scoresDataPost;
    } catch (error) {
        alert(error);
    }
};

const signOut = () => {
    sessionStorage.removeItem("id");
    sessionStorage.setItem("cart", JSON.stringify([]));
    window.location.href = "Products.html";
};