// NBA Admin Panel JavaScript

(function($) {
    "use strict";

    // Toggle the side navigation
    $("#sidebarToggle, #sidebarToggleTop").on('click', function(e) {
        $("body").toggleClass("sidebar-toggled");
        $(".sidebar").toggleClass("toggled");
        if ($(".sidebar").hasClass("toggled")) {
            $('.sidebar .collapse').collapse('hide');
        }
    });

    // Close any open menu accordions when window is resized below 768px
    $(window).resize(function() {
        if ($(window).width() < 768) {
            $('.sidebar .collapse').collapse('hide');
            $("body").addClass("sidebar-toggled");
            $(".sidebar").addClass("toggled");
        }
        
        // Ensure the toggle is correctly configured
        if ($(window).width() < 480) {
            $(".sidebar").addClass("toggled");
        }
    });

    // Prevent the content wrapper from scrolling when the fixed side navigation hovered over
    $('body.fixed-nav .sidebar').on('mousewheel DOMMouseScroll wheel', function(e) {
        if ($(window).width() > 768) {
            var e0 = e.originalEvent,
                delta = e0.wheelDelta || -e0.detail;
            this.scrollTop += (delta < 0 ? 1 : -1) * 30;
            e.preventDefault();
        }
    });

    // Scroll to top button appear
    $(document).on('scroll', function() {
        var scrollDistance = $(this).scrollTop();
        if (scrollDistance > 100) {
            $('.scroll-to-top').fadeIn();
        } else {
            $('.scroll-to-top').fadeOut();
        }
    });

    // Smooth scrolling using jQuery easing
    $(document).on('click', 'a.scroll-to-top', function(e) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top)
        }, 1000, 'easeInOutExpo');
        e.preventDefault();
    });

    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Initialize popovers
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });

    // Auto-hide alerts
    setTimeout(function() {
        $('.alert').fadeOut('slow');
    }, 5000);

    // Card hover effects
    $('.stats-card').hover(
        function() {
            $(this).find('i').addClass('fa-bounce');
        },
        function() {
            $(this).find('i').removeClass('fa-bounce');
        }
    );

    // Basketball animation for sidebar brand
    $('.sidebar-brand-icon').click(function() {
        $(this).find('i').addClass('fa-spin');
        setTimeout(() => {
            $(this).find('i').removeClass('fa-spin');
        }, 2000);
    });

    // Search functionality
    $('#searchInput').on('keyup', function() {
        var value = $(this).val().toLowerCase();
        $('.searchable-item').filter(function() {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });

    // Dynamic counter animation for stats cards
    function animateValue(obj, start, end, duration) {
        let startTimestamp = null;
        const step = (timestamp) => {
            if (!startTimestamp) startTimestamp = timestamp;
            const progress = Math.min((timestamp - startTimestamp) / duration, 1);
            obj.innerHTML = Math.floor(progress * (end - start) + start);
            if (progress < 1) {
                window.requestAnimationFrame(step);
            }
        };
        window.requestAnimationFrame(step);
    }

    // Animate stats on page load
    $(document).ready(function() {
        $('.stats-card .h5').each(function() {
            var finalValue = parseInt($(this).text());
            if (!isNaN(finalValue)) {
                $(this).text('0');
                animateValue(this, 0, finalValue, 2000);
            }
        });
    });

    // Add active class to current navigation item
    var currentPath = window.location.pathname;
    $('.sidebar .nav-link').each(function() {
        var href = $(this).attr('href');
        if (href && currentPath.includes(href)) {
            $(this).closest('.nav-item').addClass('active');
        }
    });

    // Collapse/Expand sidebar sections
    $('.nav-link[data-bs-toggle="collapse"]').on('click', function() {
        var target = $(this).attr('data-bs-target');
        $(target).on('shown.bs.collapse', function() {
            $(this).parent().find('.nav-link').first().addClass('active');
        });
        $(target).on('hidden.bs.collapse', function() {
            $(this).parent().find('.nav-link').first().removeClass('active');
        });
    });

    // Custom loading animation
    function showLoading() {
        $('body').append('<div class="loading-overlay"><div class="loading-spinner"><i class="fas fa-basketball-ball fa-spin fa-3x text-primary"></i></div></div>');
    }

    function hideLoading() {
        $('.loading-overlay').remove();
    }

    // AJAX form submissions with loading
    $(document).on('submit', 'form.ajax-form', function(e) {
        e.preventDefault();
        showLoading();
        
        var form = $(this);
        var url = form.attr('action');
        var method = form.attr('method') || 'POST';
        var data = form.serialize();

        $.ajax({
            url: url,
            method: method,
            data: data,
            success: function(response) {
                hideLoading();
                // Handle success
                if (response.success) {
                    toastr.success(response.message || 'Operation completed successfully!');
                    if (response.redirect) {
                        window.location.href = response.redirect;
                    }
                } else {
                    toastr.error(response.message || 'Operation failed!');
                }
            },
            error: function() {
                hideLoading();
                toastr.error('An error occurred. Please try again.');
            }
        });
    });

    // Real-time updates simulation
    function updateStats() {
        // Simulate real-time stats updates
        setInterval(function() {
            $('.stats-card .h5').each(function() {
                var currentValue = parseInt($(this).text());
                var change = Math.floor(Math.random() * 10) - 5; // Random change between -5 and 5
                var newValue = Math.max(0, currentValue + change);
                $(this).text(newValue);
            });
        }, 30000); // Update every 30 seconds
    }

    // Initialize real-time updates
    updateStats();

})(jQuery);

// Custom easing
$.easing.easeInOutExpo = function (x, t, b, c, d) {
    if (t == 0) return b;
    if (t == d) return b + c;
    if ((t /= d / 2) < 1) return c / 2 * Math.pow(2, 10 * (t - 1)) + b;
    return c / 2 * (-Math.pow(2, -10 * --t) + 2) + b;
};

// Add some CSS for loading overlay
$('<style>')
    .prop('type', 'text/css')
    .html(`
        .loading-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.7);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 9999;
        }
        .loading-spinner {
            text-align: center;
            color: white;
        }
        .fa-bounce {
            animation: bounce 1s infinite;
        }
    `)
    .appendTo('head');
