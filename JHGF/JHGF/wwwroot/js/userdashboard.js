// Show/hide views
function showView(viewId) {
    document.querySelectorAll('.main-content > div').forEach(div => {
        div.style.display = 'none';
    });
    document.getElementById(viewId).style.display = 'block';
    hideSuccessMessage();
}

function showDashboard() {
    showView('dashboardView');
    updateActiveMenuItem('dashboard');
}

function showProfile() {
    showView('profileView');
    updateActiveMenuItem('profile');
}

function showEditProfile() {
    showView('editProfileView');
    updateActiveMenuItem('editProfile');
}

function showBookings() {
    showView('bookingsView');
    updateActiveMenuItem('bookings');
}

function showWishlist() {
    showView('wishlistView');
    updateActiveMenuItem('wishlist');
}

// Update active menu item
function updateActiveMenuItem(item) {
    document.querySelectorAll('.menu-item').forEach(menuItem => {
        menuItem.classList.remove('active');
    });

    if (item === 'dashboard') {
        document.querySelector('.menu-item:nth-child(2)').classList.add('active');
    } else if (item === 'profile') {
        document.querySelector('.menu-item:nth-child(4)').classList.add('active');
    } else if (item === 'editProfile') {
        document.querySelector('.menu-item:nth-child(5)').classList.add('active');
    } else if (item === 'bookings') {
        document.querySelector('.menu-item:nth-child(6)').classList.add('active');
    } else if (item === 'wishlist') {
        document.querySelector('.menu-item:nth-child(7)').classList.add('active');
    }
}

// Success message
function showSuccessMessage(message) {
    const successMessage = document.getElementById('successMessage');
    document.getElementById('successText').textContent = message;
    successMessage.style.display = 'flex';
    setTimeout(() => {
        successMessage.style.display = 'none';
    }, 5000);
}

function hideSuccessMessage() {
    document.getElementById('successMessage').style.display = 'none';
}

// Form submission
document.getElementById('editProfileForm').addEventListener('submit', function (e) {
    e.preventDefault();
    showSuccessMessage('Profile updated successfully!');
    showProfile();

    // Update profile view with new data
    const firstName = document.getElementById('firstName').value;
    const lastName = document.getElementById('lastName').value;
    document.querySelector('.profile-name').textContent = firstName + ' ' + lastName;
    document.querySelector('.profile-name-large').textContent = firstName + ' ' + lastName;
});

// Initialize with dashboard view
showDashboard();



// Optional: Simple front-end password match validation
document.querySelector("form").addEventListener("submit", function (e) {
    const pass = document.getElementById("NewPassword").value;
    const confirm = document.getElementById("ConfirmPassword").value;
    if (pass !== confirm) {
        e.preventDefault();
        Swal.fire({
            icon: 'error',
            title: 'Password Mismatch',
            text: 'New password and confirmation do not match!',
        });
    }
});

// Optional front-end check
document.querySelector("form").addEventListener("submit", function (e) {
    const newPass = document.getElementById("NewPassword").value;
    const confirmPass = document.getElementById("ConfirmPassword").value;

    if (newPass || confirmPass) {
        if (newPass !== confirmPass) {
            e.preventDefault();
            Swal.fire({
                icon: 'error',
                title: 'Password Mismatch',
                text: 'New password and confirmation do not match!'
            });
        }
    }
});

@if (TempData["Error"] != null) {
    <text>
        Swal.fire({
            icon: 'error',
        title: 'Error',
        text: '@TempData["Error"]'
                    });
    </text>
}

@if (TempData["Success"] != null) {
    <text>
        Swal.fire({
            icon: 'success',
        title: 'Success',
        text: '@TempData["Success"]',
        timer: 2000,
        showConfirmButton: false
                    });
    </text>
}