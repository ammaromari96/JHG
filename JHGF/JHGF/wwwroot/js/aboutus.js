// Animation on scroll
document.addEventListener('DOMContentLoaded', function () {
    // Animate elements when they come into view
    const animateOnScroll = () => {
        const elements = document.querySelectorAll('.animate');

        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.style.animation = `fadeInUp 1s ${entry.target.classList.contains('delay-1') ? '0.2s' :
                        entry.target.classList.contains('delay-2') ? '0.4s' :
                            entry.target.classList.contains('delay-3') ? '0.6s' :
                                entry.target.classList.contains('delay-4') ? '0.8s' : '0s'} forwards`;
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });

        elements.forEach(element => {
            observer.observe(element);
        });
    };

    // Counter animation for stats
    const animateCounters = () => {
        const counters = document.querySelectorAll('.stat-number');
        const speed = 200;

        counters.forEach(counter => {
            const target = +counter.getAttribute('data-count');
            const count = +counter.innerText;
            const increment = target / speed;

            if (count < target) {
                counter.innerText = Math.ceil(count + increment);
                setTimeout(animateCounters, 1);
            } else {
                counter.innerText = target;
            }
        });
    };

    // Initialize animations
    animateOnScroll();

    // Start counter animation when stats section is visible
    const statsObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                animateCounters();
                statsObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.5 });

    const statsSection = document.querySelector('.stats-grid');
    if (statsSection) {
        statsObserver.observe(statsSection);
    }

    // Team member hover effect
    const teamMembers = document.querySelectorAll('.team-member');
    teamMembers.forEach(member => {
        member.addEventListener('mouseenter', () => {
            const img = member.querySelector('.member-image img');
            img.style.transform = 'scale(1.1)';
        });

        member.addEventListener('mouseleave', () => {
            const img = member.querySelector('.member-image img');
            img.style.transform = 'scale(1)';
        });
    });
});