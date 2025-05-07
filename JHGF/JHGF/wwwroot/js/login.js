document.addEventListener("DOMContentLoaded", function () {
    const registerButton = document.getElementById("register");
    const loginButton = document.getElementById("login");
    const container = document.getElementById("container");
    const registerForm = document.getElementById("registerForm");
    const loginForm = document.getElementById("loginForm");
    registerButton.addEventListener("click", (event) => {
        event.preventDefault();
        container.classList.add("right-panel-active");
        registerForm.scrollIntoView({ behavior: "smooth" });
    });
    loginButton.addEventListener("click", (event) => {
        event.preventDefault();
        container.classList.remove("right-panel-active");
        loginForm.scrollIntoView({ behavior: "smooth" });
    });

    registerForm.addEventListener("submit", (event) => {
        event.preventDefault();


        //registration success
        document.getElementById("registerMessage").style.display = "block";
        setTimeout(() => {
            container.classList.remove("right-panel-active");
            loginForm.scrollIntoView({ behavior: "smooth" });
        }, 2000);
    });

});

function togglePasswordVisibility(buttonId, inputId, iconId) {
    const passwordInput = document.getElementById(inputId);
    const toggleButton = document.getElementById(buttonId);
    const icon = document.getElementById(iconId);

    toggleButton.addEventListener('click', function (e) {
        // Toggle the type attribute
        const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordInput.setAttribute('type', type);

        // Toggle the icon
        if (type === 'password') {
            icon.classList.remove('fa-eye-slash');
            icon.classList.add('fa-eye');
        } else {
            icon.classList.remove('fa-eye');
            icon.classList.add('fa-eye-slash');
        }
    });
}

togglePasswordVisibility('toggleLoginPassword', 'loginPassword', 'loginIcon');
togglePasswordVisibility('toggleRegisterPassword', 'registerPassword', 'registerIcon');



function checkPasswordStrength() {
    const password = document.getElementById("registerPassword").value;
    const strengthMsg = document.getElementById("passwordStrengthMsg");
    let strength = "Weak";
    let color = "red";

    if (password.length >= 8 && /[A-Z]/.test(password) && /[a-z]/.test(password) && /[0-9]/.test(password) && /[!@#\$%\^\&*\)\(+=._-]/.test(password)) {
        strength = "Strong";
        color = "green";
    } else if (password.length >= 6 && ((/[A-Z]/.test(password) && /[a-z]/.test(password)) || /[0-9]/.test(password))) {
        strength = "Medium";
        color = "orange";
    }

    strengthMsg.textContent = `Password strength: ${strength}`;
    strengthMsg.style.color = color;
}

document.addEventListener("DOMContentLoaded", function () {
    const registerButton = document.getElementById("register");
    const loginButton = document.getElementById("login");
    const container = document.getElementById("container");

    registerButton.addEventListener("click", (event) => {
        event.preventDefault();
        container.classList.add("right-panel-active");
        document.getElementById("registerForm").scrollIntoView({ behavior: "smooth" });
    });

    loginButton.addEventListener("click", (event) => {
        event.preventDefault();
        container.classList.remove("right-panel-active");
        document.getElementById("loginForm").scrollIntoView({ behavior: "smooth" });
    });

    // Form validation and submission
    document.getElementById("registerForm").addEventListener("submit", function (event) {
        event.preventDefault();
        validateForm("register");
    });

    //Form validation for registration name
    document.getElementById("registerName").addEventListener("keypress", function (event) {
        var char = String.fromCharCode(event.which);
        if (!(/[a-zA-Z]/.test(char))) {
            event.preventDefault();
        }
    });

    document.getElementById("loginForm").addEventListener("submit", function (event) {
        event.preventDefault();
        validateForm("login");
    });

    function validateForm(formType) {
        let email, password, name;
        const spcharRegex = /[<>"/]/;
        if (formType === "register") {
            name = document.getElementById("registerName").value;
            email = document.getElementById("registerEmail").value;
            password = document.getElementById("registerPassword").value;
            if (name === "" || email === "" || password === "") {
                alert("Please fill in all fields");
                return;
            }
            if (!validateEmail(email)) {
                alert("Please enter a valid email address");
                return;
            }
            if (spcharRegex.test(name) || spcharRegex.test(password)) {
                alert('Name Or Password Cannot Contain <,>,", or /');
                return;
            }

            if (localStorage.getItem("email") === email) {
                registerDeclineBanner.style.display = "block";
                setTimeout(() => {
                    registerDeclineBanner.style.display = "none";
                }, 6000);
            }

            localStorage.setItem("name", name);
            localStorage.setItem("email", email);
            localStorage.setItem("password", password);

            registerSuccessBanner.style.display = "block";
            setTimeout(() => {
                registerSuccessBanner.style.display = "none";
                document.getElementById("registerName").value = "";
                document.getElementById("registerEmail").value = "";
                document.getElementById("registerPassword").value = "";
            }, 3000);
        } else {
            email = document.getElementById("loginEmail").value;
            password = document.getElementById("loginPassword").value;
            if (email === "" || password === "") {
                alert("Please fill in all fields");
                return;
            }
            if (!validateEmail(email)) {
                alert("Please enter a valid email address");
                return;
            }
            if (spcharRegex.test(password)) {
                alert('Password Cannot Contain <,>,", or /');
                return;
            }

            const storedEmail = localStorage.getItem("email");
            const storedPassword = localStorage.getItem("password");

            if (email === storedEmail && password === storedPassword) {
                loginSuccessBanner.style.display = "block";
                setTimeout(() => {
                    loginSuccessBanner.style.display = "none";
                    document.getElementById("loginEmail").value = "";
                    document.getElementById("loginPassword").value = "";
                    window.location.href = "profile.html"; // Redirect to Profile Page
                }, 3000);


            } else {
                loginDeclineBanner.style.display = "block";
                setTimeout(() => {
                    loginDeclineBanner.style.display = "none";
                }, 6000);
            }
        }
    }

    function validateEmail(email) {
        const re = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return re.test(email);
    }


}
);


