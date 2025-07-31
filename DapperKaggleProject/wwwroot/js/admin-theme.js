/* NBA Admin Theme JavaScript */

(function($) {
    "use strict"; // Start of use strict

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
        }
        
        // Toggle the side navigation when window is resized below 480px
        if ($(window).width() < 480 && !$(".sidebar").hasClass("toggled")) {
            $("body").addClass("sidebar-toggled");
            $(".sidebar").addClass("toggled");
            $('.sidebar .collapse').collapse('hide');
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

    // Calendar functionality
    $('.calendar-day.has-game').on('click', function() {
        var opponent = $(this).data('opponent');
        var time = $(this).data('time');
        var isHome = $(this).hasClass('home-game');
        var gameType = isHome ? 'vs' : '@';
        
        // Show game details modal or tooltip
        showGameDetails(opponent, time, gameType);
    });

    function showGameDetails(opponent, time, gameType) {
        // Create a simple tooltip or modal with game details
        var gameInfo = `
            <div class="game-tooltip">
                <h6>${gameType} ${opponent}</h6>
                <p>Time: ${time}</p>
                <small>Click to view game details</small>
            </div>
        `;
        
        // You can implement a more sophisticated modal here
        console.log('Game Details:', { opponent, time, gameType });
    }

    // Stats card hover effects
    $('.stats-card').hover(
        function() {
            $(this).find('.fa-2x').addClass('text-primary');
        },
        function() {
            $(this).find('.fa-2x').removeClass('text-primary');
        }
    );

    // Team ranking item click handlers
    $('.team-ranking-item').on('click', function() {
        var teamName = $(this).find('.team-name').text();
        console.log('Selected team:', teamName);
        // You can add navigation to team details page here
    });

    // Activity timeline animations
    function animateActivityItems() {
        $('.activity-item').each(function(index) {
            $(this).delay(index * 100).animate({
                opacity: 1,
                left: 0
            }, 500);
        });
    }

    // Initialize activity items animation
    $('.activity-item').css({ opacity: 0, position: 'relative', left: '-20px' });
    setTimeout(animateActivityItems, 500);

    // Quick action button enhancements
    $('.btn-outline-primary, .btn-outline-info, .btn-outline-success, .btn-outline-warning').hover(
        function() {
            $(this).addClass('shadow-lg');
        },
        function() {
            $(this).removeClass('shadow-lg');
        }
    );

    // Dashboard stats counter animation
    function animateCounters() {
        $('.stats-card .h5').each(function() {
            var $this = $(this);
            var countTo = parseInt($this.text().replace(/,/g, ''));
            
            if (!isNaN(countTo)) {
                $({ countNum: 0 }).animate({
                    countNum: countTo
                }, {
                    duration: 2000,
                    easing: 'swing',
                    step: function() {
                        $this.text(Math.floor(this.countNum).toLocaleString());
                    },
                    complete: function() {
                        $this.text(countTo.toLocaleString());
                    }
                });
            }
        });
    }

    // Initialize counter animation on page load
    setTimeout(animateCounters, 1000);

    // Calendar navigation
    $('.calendar-nav .btn').on('click', function() {
        var direction = $(this).find('.fa-chevron-left').length ? 'prev' : 'next';
        navigateCalendar(direction);
    });

    function navigateCalendar(direction) {
        // This would typically make an AJAX call to get new calendar data
        console.log('Navigate calendar:', direction);
        
        // Add loading animation
        var $calendarGrid = $('.calendar-grid');
        $calendarGrid.addClass('opacity-50');
        
        setTimeout(function() {
            $calendarGrid.removeClass('opacity-50');
            // Update calendar content here
        }, 500);
    }

    // Responsive chart handling
    function handleChartResize() {
        if (window.Chart && window.Chart.instances) {
            Object.values(window.Chart.instances).forEach(chart => {
                chart.resize();
            });
        }
    }

    $(window).on('resize', handleChartResize);

    // Sidebar collapse state persistence
    if (localStorage.getItem('sidebarToggled') === 'true') {
        $("body").addClass("sidebar-toggled");
        $(".sidebar").addClass("toggled");
    }

    $("#sidebarToggle, #sidebarToggleTop").on('click', function() {
        var isToggled = $("body").hasClass("sidebar-toggled");
        localStorage.setItem('sidebarToggled', isToggled);
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

    // Page loading animation
    $('body').addClass('fade-in');

    // Custom dropdown enhancements
    $('.dropdown-toggle').on('show.bs.dropdown', function() {
        $(this).closest('.dropdown').addClass('show-dropdown');
    });

    $('.dropdown-toggle').on('hide.bs.dropdown', function() {
        $(this).closest('.dropdown').removeClass('show-dropdown');
    });

    // Data Access Toggle Functionality
    $('#toggleInput').on('change', function() {
        const isChecked = $(this).is(':checked');
        const body = $('body');
        
        if (isChecked) {
            // Switch to Dapper theme (red)
            body.removeClass('ef-core-theme').addClass('dapper-theme');
            localStorage.setItem('dataAccessMode', 'dapper');
        } else {
            // Switch to EF Core theme (blue)
            body.removeClass('dapper-theme').addClass('ef-core-theme');
            localStorage.setItem('dataAccessMode', 'efcore');
        }
        
        // Trigger theme change event
        $(document).trigger('themeChanged', isChecked ? 'dapper' : 'efcore');
    });

    // Load saved theme preference
    const savedMode = localStorage.getItem('dataAccessMode') || 'efcore';
    if (savedMode === 'dapper') {
        $('#toggleInput').prop('checked', true);
        $('body').addClass('dapper-theme');
    } else {
        $('#toggleInput').prop('checked', false);
        $('body').addClass('ef-core-theme');
    }

    // NBA Theme specific functions
    window.NBAAdmin = {
        // Update team calendar
        updateTeamCalendar: function(teamId, month, year) {
            console.log('Updating calendar for team:', teamId, 'Month:', month, 'Year:', year);
            // Implementation for updating calendar data
        },

        // Show player stats
        showPlayerStats: function(playerId) {
            console.log('Showing stats for player:', playerId);
            // Implementation for showing player statistics
        },

        // Navigate to team details
        goToTeamDetails: function(teamId) {
            console.log('Navigating to team details:', teamId);
            // Implementation for team navigation
        },

        // Refresh dashboard data
        refreshDashboard: function() {
            console.log('Refreshing dashboard data...');
            // Implementation for refreshing dashboard
            $('.stats-card').addClass('refreshing');
            
            setTimeout(function() {
                $('.stats-card').removeClass('refreshing');
            }, 1500);
        },

        // Initialize real-time updates
        initRealTimeUpdates: function() {
            // Implementation for real-time data updates
            setInterval(function() {
                // Check for updates
            }, 30000); // Check every 30 seconds
        },

        // Toggle theme between EF Core and Dapper
        toggleTheme: function(isDapper) {
            const body = $('body');
            
            if (isDapper) {
                // Switch to Dapper theme (Red)
                body.removeClass('ef-core-theme').addClass('dapper-theme');
                
                // Add theme transition effect
                $('.sidebar, .dashboard-header, .topbar').addClass('theme-transition');
                setTimeout(() => {
                    $('.sidebar, .dashboard-header, .topbar').removeClass('theme-transition');
                }, 600);
                
                console.log('⚡ Switched to Dapper theme (Red)');
            } else {
                // Switch to EF Core theme (Blue)
                body.removeClass('dapper-theme').addClass('ef-core-theme');
                
                // Add theme transition effect
                $('.sidebar, .dashboard-header, .topbar').addClass('theme-transition');
                setTimeout(() => {
                    $('.sidebar, .dashboard-header, .topbar').removeClass('theme-transition');
                }, 600);
                
                console.log('🏀 Switched to EF Core theme (Blue)');
            }
        },

        // Basketball animation effects
        addBasketballEffects: function() {
            // Add floating basketball effects on sidebar
            setTimeout(() => {
                $('.sidebar').addClass('basketball-active');
            }, 1000);
            
            // Add click effects to table info cards
            $('.table-info-card').on('click', function() {
                $(this).addClass('card-bounce');
                setTimeout(() => {
                    $(this).removeClass('card-bounce');
                }, 600);
            });
            
            // Add hover effects to nav cards
            $('.nav-card').on('mouseenter', function() {
                $(this).find('.nav-icon').addClass('icon-spin');
            }).on('mouseleave', function() {
                $(this).find('.nav-icon').removeClass('icon-spin');
            });
            
            // Add pulse effect to active nav items
            $('.nav-link.active').addClass('pulse-active');
        },

        // Initialize enhanced UI effects
        initUIEffects: function() {
            // Random basketball float effect
            setInterval(() => {
                if (Math.random() > 0.7) {
                    $('.sidebar-brand-icon').addClass('random-bounce');
                    setTimeout(() => {
                        $('.sidebar-brand-icon').removeClass('random-bounce');
                    }, 600);
                }
            }, 5000);
            
            // Card statistics animation
            $('.table-info-card .badge').each(function(index) {
                setTimeout(() => {
                    $(this).addClass('count-up-animation');
                }, index * 200);
            });
        }
    };

    // Data Access Toggle Handler
    $('#toggleInput').on('change', function() {
        var isDapper = $(this).is(':checked');
        NBAAdmin.toggleTheme(isDapper);
    });

    // Initialize NBA Admin functionality when document is ready
    $(document).ready(function() {
        NBAAdmin.initRealTimeUpdates();
        NBAAdmin.addBasketballEffects();
        NBAAdmin.initUIEffects();
        
        // Set default theme (EF Core - Blue)
        NBAAdmin.toggleTheme(false);
        
        console.log('🏀 NBA Admin Panel initialized with basketball animations!');
    });

})(jQuery); // End of use strict