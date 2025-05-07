// Form submission handling
document.addEventListener('DOMContentLoaded', function () {
    const contactForm = document.getElementById('contactForm');

    if (contactForm) {
        contactForm.addEventListener('submit', function (e) {
            e.preventDefault();

            // Get form values
            const name = document.getElementById('name').value;
            const email = document.getElementById('email').value;
            const subject = document.getElementById('subject').value;
            const message = document.getElementById('message').value;

            // Here you would typically send the data to your server
            // For demonstration, we'll just show an alert
            alert(`Thank you, ${name}! Your message about "${subject}" has been received. We'll contact you at ${email} shortly.`);

            // Reset form
            contactForm.reset();
        });
    }

    // Animation on scroll
    const animateOnScroll = () => {
        const elements = document.querySelectorAll('.animate');

        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.style.animation = `fadeIn 1s ${entry.target.classList.contains('delay-1') ? '0.2s' :
                        entry.target.classList.contains('delay-2') ? '0.4s' : '0s'} forwards`;
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });

        elements.forEach(element => {
            observer.observe(element);
        });
    };

    animateOnScroll();
});




document.addEventListener("DOMContentLoaded", function () {
    const successMessage = document.querySelector('.alert-success');
    const errorMessage = document.querySelector('.alert-danger');

    if (successMessage) {
        Swal.fire({
            icon: 'success',
            title: 'Success!',
            text: successMessage.textContent.trim(),
            timer: 3000,
            showConfirmButton: false
        });
    }

    if (errorMessage) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: errorMessage.textContent.trim(),
            timer: 3000,
            showConfirmButton: false
        });
    }
});
