document.addEventListener('DOMContentLoaded', function () {
    // Smooth scroll for all links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            document.querySelector(this.getAttribute('href')).scrollIntoView({
                behavior: 'smooth'
            });
        });
    });

    // Parallax effect for hero section
    window.addEventListener('scroll', function () {
        const hero = document.querySelector('.hero');
        const scrollPosition = window.pageYOffset;
        hero.style.backgroundPositionY = scrollPosition * 0.5 + 'px';
    });

    // Typewriter effect for the hero title (optional)
    const heroTitle = document.querySelector('.hero h1');
    if (heroTitle) {
        const text = "Discover Jordan's Hidden Gems";
        heroTitle.textContent = '';
        let i = 0;
        const typing = setInterval(() => {
            if (i < text.length) {
                heroTitle.textContent += text.charAt(i);
                i++;
            } else {
                clearInterval(typing);
            }
        }, 100);
    }
});



//document.addEventListener('DOMContentLoaded', function () {
//    // Smooth scroll for the scroll down button
//    document.querySelector('.hero-scroll-down').addEventListener('click', function (e) {
//        e.preventDefault();
//        document.querySelector('#content').scrollIntoView({
//            behavior: 'smooth'
//        });
//    });

//    // Parallax effect for hero section
//    window.addEventListener('scroll', function () {
//        const hero = document.querySelector('.hero');
//        const scrollPosition = window.pageYOffset;
//        hero.style.backgroundPositionY = scrollPosition * 0.5 + 'px';
//    });

//    // Typewriter effect for the hero title (optional)
//    const heroTitle = document.querySelector('.hero h1');
//    if (heroTitle) {
//        const text = "Discover Jordan's Hidden Gems";
//        heroTitle.textContent = '';
//        let i = 0;
//        const typing = setInterval(() => {
//            if (i < text.length) {
//                heroTitle.textContent += text.charAt(i);
//                i++;
//            } else {
//                clearInterval(typing);
//            }
//        }, 100);
//    }

//    // Tab functionality for booking box
//    const tabBtns = document.querySelectorAll('.tab-btn');
//    tabBtns.forEach(btn => {
//        btn.addEventListener('click', function () {
//            // Remove active class from all buttons and tabs
//            tabBtns.forEach(btn => btn.classList.remove('active'));
//            document.querySelectorAll('.tab-content').forEach(tab => {
//                tab.classList.remove('active');
//            });

//            // Add active class to clicked button and corresponding tab
//            this.classList.add('active');
//            const tabId = this.getAttribute('data-tab') + '-tab';
//            document.getElementById(tabId).classList.add('active');
//        });
//    });

//    // Form submission handlers
//    const forms = document.querySelectorAll('.booking-form');
//    forms.forEach(form => {
//        form.addEventListener('submit', function (e) {
//            e.preventDefault();
//            // Here you would handle the form submission
//            alert('Search functionality would be implemented here!');
//        });
//    });
//});


///////////////////////////////////////secend section//////////////////////////////////////

// Filter Functionality
document.addEventListener('DOMContentLoaded', function () {
    const filterButtons = document.querySelectorAll('.filter-btn');
    const serviceCards = document.querySelectorAll('.service-card');

    filterButtons.forEach(button => {
        button.addEventListener('click', function () {
            filterButtons.forEach(btn => btn.classList.remove('active'));
            this.classList.add('active');

            const filterValue = this.getAttribute('data-filter');

            serviceCards.forEach(card => {
                card.style.display = (filterValue === 'all' || card.getAttribute('data-category') === filterValue)
                    ? 'block'
                    : 'none';
            });
        });
    });
});



///////////////////////////////////////third section//////////////////////////////////////


// Wishlist functionality
document.addEventListener('DOMContentLoaded', function () {
    const wishlistButtons = document.querySelectorAll('.wishlist-button');

    wishlistButtons.forEach(button => {
        button.addEventListener('click', function () {
            this.classList.toggle('active');
            const icon = this.querySelector('i');
            if (this.classList.contains('active')) {
                icon.classList.remove('far');
                icon.classList.add('fas');
            } else {
                icon.classList.remove('fas');
                icon.classList.add('far');
            }
        });
    });

    // Intersection Observer for scroll animations
    const animateOnScroll = () => {
        const cards = document.querySelectorAll('.destination-card');

        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.style.animation = `fadeInUp 0.8s ${entry.target.dataset.delay || '0s'} forwards`;
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });

        cards.forEach((card, index) => {
            card.dataset.delay = `${index * 0.1}s`;
            observer.observe(card);
        });
    };

    animateOnScroll();
});



///////////////////////////////////////third section//////////////////////////////////////

// Testimonial Slider Functionality
document.addEventListener('DOMContentLoaded', function () {
    const slider = document.getElementById('slider');
    const dots = document.querySelectorAll('.slider-dot');
    let currentSlide = 0;
    const totalSlides = document.querySelectorAll('.testimonial-card').length;

    // Initialize slider
    function updateSlider() {
        slider.scrollTo({
            left: currentSlide * slider.offsetWidth,
            behavior: 'smooth'
        });

        // Update dots
        dots.forEach((dot, index) => {
            dot.classList.toggle('active', index === currentSlide);
        });
    }

    // Navigation functions
    window.moveSlide = function (step) {
        currentSlide = (currentSlide + step + totalSlides) % totalSlides;
        updateSlider();
    };

    window.goToSlide = function (index) {
        currentSlide = index;
        updateSlider();
    };

    // Auto-rotate testimonials (optional)
    let autoSlide = setInterval(() => moveSlide(1), 5000);

    // Pause on hover
    slider.addEventListener('mouseenter', () => clearInterval(autoSlide));
    slider.addEventListener('mouseleave', () => {
        autoSlide = setInterval(() => moveSlide(1), 5000);
    });

    // Touch support for mobile
    let touchStartX = 0;
    let touchEndX = 0;

    slider.addEventListener('touchstart', (e) => {
        touchStartX = e.changedTouches[0].screenX;
        clearInterval(autoSlide);
    }, { passive: true });

    slider.addEventListener('touchend', (e) => {
        touchEndX = e.changedTouches[0].screenX;
        handleSwipe();
        autoSlide = setInterval(() => moveSlide(1), 5000);
    }, { passive: true });

    function handleSwipe() {
        if (touchEndX < touchStartX - 50) moveSlide(1);
        if (touchEndX > touchStartX + 50) moveSlide(-1);
    }
});