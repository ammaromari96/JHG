document.addEventListener('DOMContentLoaded', function () {
    // Form elements
    const emailForm = document.getElementById('email-form');
    const otpForm = document.getElementById('otp-form');
    const passwordForm = document.getElementById('password-form');

    // Input fields
    const emailInput = document.getElementById('email');
    const otpInput = document.getElementById('otp');
    const newPasswordInput = document.getElementById('newPassword');
    const confirmPasswordInput = document.getElementById('confirmPassword');

    // Buttons
    const sendCodeBtn = document.getElementById('send-code-btn');
    const verifyBtn = document.getElementById('verify-btn');
    const resetBtn = document.getElementById('reset-btn');
    const backToLoginBtn = document.getElementById('back-to-login');
    const resendCodeLink = document.getElementById('resend-code-link');

    // Error messages
    const errorMessage = document.getElementById('error-message');
    const emailError = document.getElementById('email-error');
    const otpError = document.getElementById('otp-error');
    const passwordError = document.getElementById('password-error');
    const confirmError = document.getElementById('confirm-error');

    // Steps
    const step1 = document.getElementById('step1');
    const step2 = document.getElementById('step2');
    const step3 = document.getElementById('step3');
    const step4 = document.getElementById('step4');

    // Step indicators
    const step1Indicator = document.getElementById('step1-indicator');
    const step2Indicator = document.getElementById('step2-indicator');
    const step3Indicator = document.getElementById('step3-indicator');

    // Current step
    let currentStep = 1;
    let userEmail = '';

    // Email validation
    function validateEmail(email) {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email);
    }

    // Show error
    function showError(element, message) {
        element.textContent = message;
        element.style.display = 'block';
    }

    // Hide error
    function hideError(element) {
        element.style.display = 'none';
    }

    // Show step
    function showStep(stepNumber) {
        // Hide all steps
        step1.classList.add('hidden');
        step2.classList.add('hidden');
        step3.classList.add('hidden');
        step4.classList.add('hidden');

        // Show current step
        if (stepNumber === 1) step1.classList.remove('hidden');
        if (stepNumber === 2) step2.classList.remove('hidden');
        if (stepNumber === 3) step3.classList.remove('hidden');
        if (stepNumber === 4) step4.classList.remove('hidden');

        // Update step indicators
        step1Indicator.classList.remove('active', 'completed');
        step2Indicator.classList.remove('active', 'completed');
        step3Indicator.classList.remove('active', 'completed');

        if (stepNumber === 1) {
            step1Indicator.classList.add('active');
        } else if (stepNumber === 2) {
            step1Indicator.classList.add('completed');
            step2Indicator.classList.add('active');
        } else if (stepNumber === 3) {
            step1Indicator.classList.add('completed');
            step2Indicator.classList.add('completed');
            step3Indicator.classList.add('active');
        } else if (stepNumber === 4) {
            step1Indicator.classList.add('completed');
            step2Indicator.classList.add('completed');
            step3Indicator.classList.add('completed');
        }
    }

    // Email form submission
    emailForm.addEventListener('submit', function (e) {
        e.preventDefault();

        const email = emailInput.value.trim();
        userEmail = email;

        if (!email) {
            showError(emailError, 'Email is required');
            emailInput.classList.add('is-invalid');
            return;
        }

        if (!validateEmail(email)) {
            showError(emailError, 'Please enter a valid email address');
            emailInput.classList.add('is-invalid');
            return;
        }

        // Simulate API call
        sendCodeBtn.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Sending...';
        sendCodeBtn.disabled = true;

        setTimeout(function () {
            // In a real app, you would make an API call here
            sendCodeBtn.innerHTML = 'Send Verification Code';
            sendCodeBtn.disabled = false;

            // Move to OTP step directly
            currentStep = 2;
            showStep(currentStep);

            // Auto-focus OTP input
            otpInput.focus();
        }, 1500);
    });

    // Resend code
    resendCodeLink.addEventListener('click', function (e) {
        e.preventDefault();

        if (!userEmail) return;

        // Simulate resend
        resendCodeLink.textContent = 'Sending...';

        setTimeout(function () {
            resendCodeLink.textContent = 'Resend Code';
            showError(errorMessage, 'New verification code sent to your email');
            setTimeout(() => errorMessage.classList.add('hidden'), 3000);
        }, 1000);
    });

    // OTP form submission
    otpForm.addEventListener('submit', function (e) {
        e.preventDefault();

        const otp = otpInput.value.trim();

        if (!otp || otp.length !== 6) {
            showError(otpError, 'Please enter the 6-digit code');
            otpInput.classList.add('is-invalid');
            return;
        }

        // Simulate API call
        verifyBtn.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Verifying...';
        verifyBtn.disabled = true;

        setTimeout(function () {
            // In a real app, you would verify the OTP with your backend
            verifyBtn.innerHTML = 'Verify Code';
            verifyBtn.disabled = false;

            // For demo purposes, we'll assume the code is correct
            // Move to password step
            currentStep = 3;
            showStep(currentStep);
        }, 1500);
    });

    // Password form submission
    passwordForm.addEventListener('submit', function (e) {
        e.preventDefault();

        const newPassword = newPasswordInput.value;
        const confirmPassword = confirmPasswordInput.value;

        // Reset errors
        newPasswordInput.classList.remove('is-invalid');
        confirmPasswordInput.classList.remove('is-invalid');
        hideError(passwordError);
        hideError(confirmError);

        // Validate password
        if (!newPassword || newPassword.length < 8) {
            showError(passwordError, 'Password must be at least 8 characters');
            newPasswordInput.classList.add('is-invalid');
            return;
        }

        // Check if passwords match
        if (newPassword !== confirmPassword) {
            showError(confirmError, 'Passwords do not match');
            confirmPasswordInput.classList.add('is-invalid');
            return;
        }

        // Simulate API call
        resetBtn.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Resetting...';
        resetBtn.disabled = true;

        setTimeout(function () {
            // In a real app, you would send the new password to your backend
            resetBtn.innerHTML = 'Reset Password';
            resetBtn.disabled = false;

            // Move to success step
            currentStep = 4;
            showStep(currentStep);
        }, 1500);
    });

    // Back to login
    backToLoginBtn.addEventListener('click', function () {
        window.location.href = '/login';
    });

    // Input validation
    emailInput.addEventListener('input', function () {
        if (validateEmail(emailInput.value.trim())) {
            emailInput.classList.remove('is-invalid');
            hideError(emailError);
        }
    });

    otpInput.addEventListener('input', function () {
        if (otpInput.value.trim().length === 6) {
            otpInput.classList.remove('is-invalid');
            hideError(otpError);
        }
    });

    newPasswordInput.addEventListener('input', function () {
        if (newPasswordInput.value.length >= 8) {
            newPasswordInput.classList.remove('is-invalid');
            hideError(passwordError);
        }
    });

    confirmPasswordInput.addEventListener('input', function () {
        if (confirmPasswordInput.value === newPasswordInput.value) {
            confirmPasswordInput.classList.remove('is-invalid');
            hideError(confirmError);
        }
    });

    // Auto move between OTP inputs
    otpInput.addEventListener('keyup', function (e) {
        if (e.key === 'Backspace') {
            return;
        }
        if (otpInput.value.length === 6) {
            verifyBtn.focus();
        }
    });
});