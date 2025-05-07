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

// Set default date to tomorrow
const tomorrow = new Date();
tomorrow.setDate(tomorrow.getDate() + 1);
document.getElementById('startDate').valueAsDate = tomorrow;

// Form submission
document.getElementById('bookingForm').addEventListener('submit', function (e) {
    e.preventDefault();

    // Get form values
    const startDate = document.getElementById('startDate').value;
    const adults = document.getElementById('adults').value;
    const children = document.getElementById('children').value;
    const pickupLocation = document.getElementById('pickupLocation').value;

    // Calculate total price
    const adultPrice = 850;
    const childPrice = 425;
    const totalPrice = (adults * adultPrice) + (children * childPrice);

    // Show booking confirmation
    alert(`Booking confirmed! Your Classic Jordan Explorer package starts on ${startDate}. Total: $${totalPrice}`);

    // Reset form
    this.reset();
    document.getElementById('startDate').valueAsDate = tomorrow;
});

// Star rating functionality
const stars = document.querySelectorAll('.rating-star');
const ratingInput = document.getElementById('ratingValue');

stars.forEach(star => {
    star.addEventListener('click', function () {
        const value = parseInt(this.getAttribute('data-value'));
        ratingInput.value = value;

        stars.forEach((s, index) => {
            if (index < value) {
                s.classList.add('active');
            } else {
                s.classList.remove('active');
            }
        });
    });
});

// Review form submission
document.getElementById('reviewForm').addEventListener('submit', function (e) {
    e.preventDefault();

    // Get form values
    const name = document.getElementById('reviewerName').value;
    const rating = parseInt(document.getElementById('ratingValue').value);
    const title = document.getElementById('reviewTitle').value;
    const content = document.getElementById('reviewText').value;

    if (rating === 0) {
        alert('Please select a rating');
        return;
    }

    // Create new review element
    const newReview = document.createElement('div');
    newReview.className = 'review-item';

    // Get current date
    const today = new Date();
    const dateString = today.toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'long',
        day: 'numeric'
    });

    // Create stars HTML
    let starsHtml = '';
    for (let i = 0; i < 5; i++) {
        if (i < rating) {
            starsHtml += '<i class="fas fa-star"></i>';
        } else {
            starsHtml += '<i class="far fa-star"></i>';
        }
    }

    // Set review content
    newReview.innerHTML = `
                <div class="review-header">
                    <span class="review-author">${name}</span>
                    <span class="review-date">${dateString}</span>
                </div>
                <div class="review-stars">
                    ${starsHtml}
                </div>
                <h3 class="itinerary-title" style="margin-bottom: 5px;">${title}</h3>
                <p class="review-content">${content}</p>
            `;

    // Add the new review to the page
    document.getElementById('newReviews').prepend(newReview);

    // Reset the form
    this.reset();
    ratingInput.value = 0;
    stars.forEach(star => star.classList.remove('active'));

    // Show success message
    alert('Thank you for your review!');
});