//// Show/hide views
//function showView(viewId) {
//    document.querySelectorAll('.main-content > div').forEach(div => {
//        div.style.display = 'none';
//    });
//    document.getElementById(viewId).style.display = 'block';
//    hideAllAlerts();
//}

//function showDashboard() {
//    showView('dashboardView');
//    updateActiveMenuItem('dashboard');
//}

//function showHotels() {
//    showView('hotelsView');
//    updateActiveMenuItem('hotels');
//}

//function showAddHotel() {
//    showView('addHotelView');
//    updateActiveMenuItem('addHotel');
//}

//function showEditHotel() {
//    showView('editHotelView');
//    updateActiveMenuItem('editHotel');
//}

//function showTours() {
//    showView('toursView');
//    updateActiveMenuItem('tours');
//}

//function showAddTour() {
//    showView('addTourView');
//    updateActiveMenuItem('addTour');
//}

//function showEditTour() {
//    showView('editTourView');
//    updateActiveMenuItem('editTour');
//}

//function showPackages() {
//    showView('packagesView');
//    updateActiveMenuItem('packages');
//}

//function showAddPackage() {
//    showView('addPackageView');
//    updateActiveMenuItem('addPackage');
//}

//function showEditPackage() {
//    showView('editPackageView');
//    updateActiveMenuItem('editPackage');
//}

//function showUsers() {
//    showView('usersView');
//    updateActiveMenuItem('users');
//}

//function showMessages() {
//    showView('messagesView');
//    updateActiveMenuItem('messages');
//}

//// Update active menu item
//function updateActiveMenuItem(item) {
//    document.querySelectorAll('.menu-item').forEach(menuItem => {
//        menuItem.classList.remove('active');
//    });

//    const menuItems = document.querySelectorAll('.menu-item');

//    if (item === 'dashboard') {
//        menuItems[0].classList.add('active');
//    } else if (item === 'hotels') {
//        menuItems[2].classList.add('active');
//    } else if (item === 'addHotel') {
//        menuItems[3].classList.add('active');
//    } else if (item === 'editHotel') {
//        menuItems[4].classList.add('active');
//    } else if (item === 'tours') {
//        menuItems[6].classList.add('active');
//    } else if (item === 'addTour') {
//        menuItems[7].classList.add('active');
//    } else if (item === 'editTour') {
//        menuItems[8].classList.add('active');
//    } else if (item === 'packages') {
//        menuItems[10].classList.add('active');
//    } else if (item === 'addPackage') {
//        menuItems[11].classList.add('active');
//    } else if (item === 'editPackage') {
//        menuItems[12].classList.add('active');
//    } else if (item === 'users') {
//        menuItems[14].classList.add('active');
//    } else if (item === 'messages') {
//        menuItems[16].classList.add('active');
//    }
//}

//// Delete functions




//// Hide all alerts
//function hideAllAlerts() {
//    document.querySelectorAll('.alert').forEach(alert => {
//        alert.style.display = 'none';
//    });
//}

//// Form submissions
//document.getElementById('addHotelForm').addEventListener('submit', function (e) {
//    e.preventDefault();
//    document.getElementById('addHotelSuccessText').textContent = 'Hotel added successfully!';
//    document.getElementById('addHotelSuccessAlert').style.display = 'flex';
//    setTimeout(() => {
//        document.getElementById('addHotelSuccessAlert').style.display = 'none';
//    }, 5000);
//    this.reset();
//});

//document.getElementById('editHotelForm').addEventListener('submit', function (e) {
//    e.preventDefault();
//    document.getElementById('editHotelSuccessText').textContent = 'Hotel updated successfully!';
//    document.getElementById('editHotelSuccessAlert').style.display = 'flex';
//    setTimeout(() => {
//        document.getElementById('editHotelSuccessAlert').style.display = 'none';
//    }, 5000);
//    showHotels();
//});

//document.getElementById('addTourForm').addEventListener('submit', function (e) {
//    e.preventDefault();
//    document.getElementById('addTourSuccessText').textContent = 'Tour added successfully!';
//    document.getElementById('addTourSuccessAlert').style.display = 'flex';
//    setTimeout(() => {
//        document.getElementById('addTourSuccessAlert').style.display = 'none';
//    }, 5000);
//    this.reset();
//});

//document.getElementById('editTourForm').addEventListener('submit', function (e) {
//    e.preventDefault();
//    document.getElementById('editTourSuccessText').textContent = 'Tour updated successfully!';
//    document.getElementById('editTourSuccessAlert').style.display = 'flex';
//    setTimeout(() => {
//        document.getElementById('editTourSuccessAlert').style.display = 'none';
//    }, 5000);
//    showTours();
//});

//document.getElementById('addPackageForm').addEventListener('submit', function (e) {
//    e.preventDefault();
//    document.getElementById('addPackageSuccessText').textContent = 'Package added successfully!';
//    document.getElementById('addPackageSuccessAlert').style.display = 'flex';
//    setTimeout(() => {
//        document.getElementById('addPackageSuccessAlert').style.display = 'none';
//    }, 5000);
//    this.reset();
//});

//document.getElementById('editPackageForm').addEventListener('submit', function (e) {
//    e.preventDefault();
//    document.getElementById('editPackageSuccessText').textContent = 'Package updated successfully!';
//    document.getElementById('editPackageSuccessAlert').style.display = 'flex';
//    setTimeout(() => {
//        document.getElementById('editPackageSuccessAlert').style.display = 'none';
//    }, 5000);
//    showPackages();
//});

//// Initialize with dashboard view
//showDashboard();


