// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Smooth scrolling for navigation links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        document.querySelector(this.getAttribute('href')).scrollIntoView({
            behavior: 'smooth'
        });
    });
});

// Add a confirmation dialog for form submissions
document.querySelectorAll('form').forEach(form => {
    form.addEventListener('submit', function (e) {
        if (!confirm('Are you sure you want to submit this form?')) {
            e.preventDefault();
        }
    });
});
