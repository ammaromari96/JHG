// Initialize image slider
const swiper = new Swiper('.swiper', {
    loop: true,
    autoplay: {
        delay: 5000,
        disableOnInteraction: false,
    },
    pagination: {
        el: '.swiper-pagination',
        clickable: true,
    },
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
});

// Set default dates (today + tomorrow)
const today = new Date();
const tomorrow = new Date();
tomorrow.setDate(today.getDate() + 1);

document.getElementById('checkInDate').valueAsDate = today;
document.getElementById('checkOutDate').valueAsDate = tomorrow;

// Form submission
document.getElementById('bookingForm').addEventListener('submit', function (e) {
    e.preventDefault();

    // Get form values
    const checkIn = document.getElementById('checkInDate').value;
    const checkOut = document.getElementById('checkOutDate').value;
    const adults = document.getElementById('adults').value;
    const children = document.getElementById('children').value;
    432
    // Calculate nights
    const checkInDate = new Date(checkIn);
    const checkOutDate = new Date(checkOut);
    const nights = Math.round((checkOutDate - checkInDate) / (1000 * 60 * 60 * 24));
    const totalPrice = nights * 120;

    // Show payment confirmation
    alert(`Payment successful! Your booking for the Deluxe King Room has been confirmed for ${nights} nights. Total: $${totalPrice}`);

    // Reset form
    this.reset();
    document.getElementById('checkInDate').valueAsDate = today;
    document.getElementById('checkOutDate').valueAsDate = tomorrow;
});

// Date validation - check-out must be after check-in
document.getElementById('checkInDate').addEventListener('change', function () {
    const checkInDate = new Date(this.value);
    const checkOutDate = new Date(document.getElementById('checkOutDate').value);

    if (checkOutDate <= checkInDate) {
        const nextDay = new Date(checkInDate);
        nextDay.setDate(checkInDate.getDate() + 1);
        document.getElementById('checkOutDate').valueAsDate = nextDay;
    }
});