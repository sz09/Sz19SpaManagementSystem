/*!

 =========================================================
 * Material Dashboard - v1.2.0
 =========================================================

 * Product Page: http://www.creative-tim.com/product/material-dashboard
 * Copyright 2017 Creative Tim (http://www.creative-tim.com)
 * Licensed under MIT (https://github.com/creativetimofficial/material-dashboard/blob/master/LICENSE.md)

 =========================================================

 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 */


(function() {
    isWindows = navigator.platform.indexOf('Win') > -1 ? true : false;

    if (isWindows) {
        // if we are on windows OS we activate the perfectScrollbar function
        $('.sidebar .sidebar-wrapper, .main-panel').perfectScrollbar();

        $('html').addClass('perfect-scrollbar-on');
    } else {
        $('html').addClass('perfect-scrollbar-off');
    }
})();


var searchVisible = 0;
var transparent = true;

var transparentDemo = true;
var fixedTop = false;

var mobile_menu_visible = 0,
    mobile_menu_initialized = false,
    toggle_initialized = false,
    bootstrap_nav_initialized = false;

var seq = 0,
    delays = 80,
    durations = 500;
var seq2 = 0,
    delays2 = 80,
    durations2 = 500;


$(document).ready(function() {

    $sidebar = $('.sidebar');

    $.material.init();

    window_width = $(window).width();

    md.initSidebarsCheck();

    // check if there is an image set for the sidebar's background
    md.checkSidebarImage();

    //  Activate the tooltips
    $('[rel="tooltip"]').tooltip();

    $('.form-control').on("focus", function() {
        $(this).parent('.input-group').addClass("input-group-focus");
    }).on("blur", function() {
        $(this).parent(".input-group").removeClass("input-group-focus");
    });

});

$(document).on('click', '.navbar-toggle', function() {
    $toggle = $(this);

    if (mobile_menu_visible == 1) {
        $('html').removeClass('nav-open');

        $('.close-layer').remove();
        setTimeout(function() {
            $toggle.removeClass('toggled');
        }, 400);

        mobile_menu_visible = 0;
    } else {
        setTimeout(function() {
            $toggle.addClass('toggled');
        }, 430);

        div = '<div id="bodyClick"></div>';
        $(div).appendTo('body').click(function() {
            $('html').removeClass('nav-open');
            mobile_menu_visible = 0;
            setTimeout(function() {
                $toggle.removeClass('toggled');
                $('#bodyClick').remove();
            }, 550);
        });

        $('html').addClass('nav-open');
        mobile_menu_visible = 1;

    }
});

// activate collapse right menu when the windows is resized
$(window).resize(function() {
    md.initSidebarsCheck();
    // reset the seq for charts drawing animations
    seq = seq2 = 0;
});

md = {
    misc: {
        navbar_menu_visible: 0,
        active_collapse: true,
        disabled_collapse_init: 0,
    },

    checkSidebarImage: function() {
        $sidebar = $('.sidebar');
        image_src = $sidebar.data('image');

        if (image_src !== undefined) {
            sidebar_container = '<div class="sidebar-background" style="background-image: url(' + image_src + ') "/>'
            $sidebar.append(sidebar_container);
        }
    },

    checkScrollForTransparentNavbar: debounce(function() {
        if ($(document).scrollTop() > 260) {
            if (transparent) {
                transparent = false;
                $('.navbar-color-on-scroll').removeClass('navbar-transparent');
            }
        } else {
            if (!transparent) {
                transparent = true;
                $('.navbar-color-on-scroll').addClass('navbar-transparent');
            }
        }
    }, 17),

    initSidebarsCheck: function() {
        if ($(window).width() <= 991) {
            if ($sidebar.length != 0) {
                md.initRightMenu();
            }
        }
    },

    initRightMenu: debounce(function() {
        $sidebar_wrapper = $('.sidebar-wrapper');

        if (!mobile_menu_initialized) {
            $navbar = $('nav').find('.navbar-collapse').children('.navbar-nav.navbar-right');

            mobile_menu_content = '';

            nav_content = $navbar.html();

            nav_content = '<ul class="nav nav-mobile-menu">' + nav_content + '</ul>';

            navbar_form = $('nav').find('.navbar-form').get(0).outerHTML;

            $sidebar_nav = $sidebar_wrapper.find(' > .nav');

            // insert the navbar form before the sidebar list
            $nav_content = $(nav_content);
            $navbar_form = $(navbar_form);
            $nav_content.insertBefore($sidebar_nav);
            $navbar_form.insertBefore($nav_content);

            $(".sidebar-wrapper .dropdown .dropdown-menu > li > a").click(function(event) {
                event.stopPropagation();

            });

            // simulate resize so all the charts/maps will be redrawn
            window.dispatchEvent(new Event('resize'));

            mobile_menu_initialized = true;
        } else {
            if ($(window).width() > 991) {
                // reset all the additions that we made for the sidebar wrapper only if the screen is bigger than 991px
                $sidebar_wrapper.find('.navbar-form').remove();
                $sidebar_wrapper.find('.nav-mobile-menu').remove();

                mobile_menu_initialized = false;
            }
        }
    }, 200),


    startAnimationForLineChart: function(chart) {

        chart.on('draw', function(data) {
            if (data.type === 'line' || data.type === 'area') {
                data.element.animate({
                    d: {
                        begin: 600,
                        dur: 700,
                        from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
                        to: data.path.clone().stringify(),
                        easing: Chartist.Svg.Easing.easeOutQuint
                    }
                });
            } else if (data.type === 'point') {
                seq++;
                data.element.animate({
                    opacity: {
                        begin: seq * delays,
                        dur: durations,
                        from: 0,
                        to: 1,
                        easing: 'ease'
                    }
                });
            }
        });

        seq = 0;
    },
    startAnimationForBarChart: function(chart) {

        chart.on('draw', function(data) {
            if (data.type === 'bar') {
                seq2++;
                data.element.animate({
                    opacity: {
                        begin: seq2 * delays2,
                        dur: durations2,
                        from: 0,
                        to: 1,
                        easing: 'ease'
                    }
                });
            }
        });

        seq2 = 0;
    }
}


// Returns a function, that, as long as it continues to be invoked, will not
// be triggered. The function will be called after it stops being called for
// N milliseconds. If `immediate` is passed, trigger the function on the
// leading edge, instead of the trailing.

function debounce(func, wait, immediate) {
    var timeout;
    return function() {
        var context = this,
            args = arguments;
        clearTimeout(timeout);
        timeout = setTimeout(function() {
            timeout = null;
            if (!immediate) func.apply(context, args);
        }, wait);
        if (immediate && !timeout) func.apply(context, args);
    };
};

// This function to set show which tab choosen in menu
$(document).ready(function () {
    let currentURL = window.location.href.toLowerCase();
    $(".nav-left>.in-nav-left>.sidebar>.sidebar-wrapper>.nav>.active").removeClass("active");
    if (currentURL.includes("admin")) {
        let urlLengthTemp = currentURL.substring(0, currentURL.lastIndexOf("admin") + 5).length;
        if (currentURL.substring(urlLengthTemp).length <= 1)
            $("#li-for-dashboard").addClass("active");
        else if (currentURL.includes("/employee") || currentURL.includes("/createemployee"))
            $("#li-for-employees").addClass("active");
        else if (currentURL.includes("/typography"))
            $("#li-for-typography").addClass("active");
        else if (currentURL.includes("/adminservices") || currentURL.includes("/createservice"))
            $("#li-for-servicesadmin").addClass("active");
        else if (currentURL.includes("/notifications"))
            $("#li-for-notifications").addClass("active");
        else if (currentURL.includes("/salaries"))
            $("#li-for-salary").addClass("active");
        else if (currentURL.includes("salary"))
            $("#li-for-salary").addClass("active");
        else if (currentURL.includes("book"))
            $("#li-for-booking").addClass("active");
         else if (currentURL.includes("bed"))
            $("#li-for-bed").addClass("active");
    }
});

// Direction
$(document).ready(function () {
    let currentUrl = window.location.href.toLowerCase();
    if (currentUrl.includes('admin')) {
        let a = currentURL.indexOf('admin');
        let urlTemp = currentUrl.substring(0, currentURL.indexOf('admin') + 5);
        $('li-for-booking').children('a').href = urlTemp + '/bookings';
        $('li-for-employees').children('a').href = urlTemp + '/employees';
        $('li-for-salary').children('a').href = urlTemp + '/salaries';
        $('li-for-servicesadmin').children('a').href = urlTemp + '/servicesadmin';
        $('li-for-bed').children('a').href = urlTemp + '/beds';
        $('li-for-notifications').children('a').href = urlTemp + '/notifications';
    }
});

// Function to expand notification
$(document).on("click", "a[data-toggle='dropdown'][aria-expanded='false']", function () {
    if(!$(this).parent().hasClass('open'))
        $(this).parent().addClass('open');
    else 
        $(this).parent().removeClass('open');
});

// Function to Sign In 
function SignIn() {
    let currentURL = $.currentURL;

    $.ajax({
        type: 'post',
        dataType: 'json',
        async: false,
        data: {
            username: $('#username').val(),
            password: $('#password').val(),

        },
        url: './account/checksignin',
        success: function (msg) {
            var jsonparsed = JSON.parse(msg);
            if (jsonparsed.check == false) {
                
            }
            else if (jsonparsed.check == true) {
                    // Check be admin role and come to admin manage
                    // Dashboard will showing
                if (jsonparsed.role == "admin") {
                    window.location.href = "./admin";
                }
                    // Check be customer role
                    // Come the general page with role
                else if (jsonparsed.includes("customer")){

                }
                // 
                else if (jsonparsed.includes("otheruser")) {

                }
            }
            else
                console.log("else end");
        },
        error: function () {
           
        },
        complete: function (rst) {
            if (rst.success) {

            }
            else {
            }
        }
    });
}

// Create new profile
$(document).on('click', '#create-profile-main-action', function () {
    if ((window.location.href).includes('admin')) {
        Loading();
        $.ajax({
            type: 'post',
            dataType: 'json',
            async: false,
            data: {
                empCode:  $('#StaffCode').val(),
                empFirstName: $('#FirstName').val(),
                empLastMiddle: $('#LastMiddle').val(),
                empIdentifierNumber: $('#IdentifierNumber').val(),
                empAddress: $('#ContactInformation_Address_AddressNumberNoAndStreet').val(),
                empDistrict: $('#ContactInformation_Address_District_DistrictName').val(),
                empCity: $('#ContactInformation_Address_District_Province_ProvinceName').val(),
                empCountry: $('#ContactInformation_Address_District_Province_Country_CountryName').val(),
                empPhone: $('#ContactInformation_EAddress_NumberPhone').val(),
                empEmail: $('#ContactInformation_EAddress_Email').val(),
                empWebsite: $('#ContactInformation_EAddress_Website').val(),
                empSummary: $('#Summary').val()
            },
            url: '/admin/SumitAddEmployee',
            complete: function (xhr) {
                $('#alert-create-emp').removeClass('hidden');
                $('#loading').empty();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                flag_create_emp = false;
                $('#main-alert').addClass('alert-danger');
                $('#description').html('Add employee fail');
            },
            success: function (value) {
                let data = JSON.parse(value);
                if (data.insertEmp == true) {
                    flag_create_emp = true;
                    if (data.updateContactInformation == true) {
                        $('#main-alert').addClass('alert-success');
                        $('#description').html('Success add employee to database');
                        $("#change-url").html('Click here to show all employee');
                        url_create = './employees';
                    }
                    else{
                        $('#main-alert').addClass('alert-warning');
                        $('#description').html('Success add employee to database.');
                        $("#change-url").html('Click here to update contact information');
                        url_create = "./employee?id=" + data.personid;
                    }
                }
                else {
                    $('#main-alert').addClass('alert-danger');
                    $('#description').html('Add employee fail');
                }
            }
        });
        $('.main-panel').animate({
            scrollTop: (0, 0)
        }, 1500);
    }
});

//Update employee
$(document).on('click', '#update-prifile-main', function () {
    if ((window.location.href).includes('admin')) {
        Loading();
        $.ajax({
            type: 'post',
            dataType: 'json',
            async: false,
            data: {
                id: $('#Id').val(),
                empCode: $('#StaffCode').val(),
                empFirstName: $('#FirstName').val(),
                empLastMiddle: $('#LastMiddle').val(),
                empIdentifierNumber: $('#IdentifierNumber').val(),
                empAddress: $('#ContactInformation_Address_AddressNumberNoAndStreet').val(),
                empDistrict: $('#ContactInformation_Address_District_DistrictName').val(),
                empCity: $('#ContactInformation_Address_District_Province_ProvinceName').val(),
                empCountry: $('#ContactInformation_Address_District_Province_Country_CountryName').val(),
                empPhone: $('#ContactInformation_EAddress_NumberPhone').val(),
                empEmail: $('#ContactInformation_EAddress_Email').val(),
                empWebsite: $('#ContactInformation_EAddress_Website').val(),
                empSummary: $('#Summary').val()
            },
            url: '/admin/SubmitUpdateEmployee',
            complete: function (xhr) {
                $('#alert-create-emp').removeClass('hidden');
                $('#loading').empty();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                flag_create_emp = false;
                $('#main-alert').addClass('alert-danger');
                $('#description').html('Update employee fail');
            },
            success: function (value) {
                let data = JSON.parse(value);
                if (data.insertEmp == true) {
                    flag_create_emp = true;
                    if (data.updateContactInformation == true) {
                        $('#main-alert').addClass('alert-success');
                        $('#description').html('Success update employee to database');
                        $("#change-url").html('Click here to show all employees');
                        url_create = './employees';
                    }
                    else {
                        $('#main-alert').addClass('alert-warning');
                        $('#description').html('Success add employee to database.');
                        $("#change-url").html('Click here to update contact information');
                        url_create = "./employee?id=" + data.personid;
                    }
                }
                else {
                    $('#main-alert').addClass('alert-danger');
                    $('#description').html('Update employee fail');
                }
            }
        });
        $('.main-panel').animate({
            scrollTop: (0, 0)
        }, 1500);
    }
});

function GoToEmp(id) {
    window.location.href = "./employee?id=" + id;
}

$(document).on('click', '#btn-go-to-create-profile', function () {
    if((window.location.href).includes('admin')) {
        window.location.href = "./createemployee";
    }
});
let url_create = "";
// Alert 
$(document).on('click', '#alert-create-emp', function () {
    let promise = $.ajax({
        function: $(this).hide(1000)
    });
    promise.done(function () {
        window.location.href = url_create;
    });
   
});
// End alert

// Loading
function Loading() {
    let html =  "<div class='windows8'>" + 
	                "<div class='wBall' id='wBall_1'>" + 
	                    "<div class='wInnerBall'></div>" + 
	                "</div>" + 
	                "<div class='wBall' id='wBall_2'>" + 
                        "<div class='wInnerBall'></div>"  + 
	                "</div>" + 
	                "<div class='wBall' id='wBall_3'>" + 
		                "<div class='wInnerBall'></div>" + 
	                "</div>" + 
	                "<div class='wBall' id='wBall_4'>" + 
		                "<div class='wInnerBall'></div>" + 
	                "</div>" + 
	                "<div class='wBall' id='wBall_5'>" + 
		                "<div class='wInnerBall'></div>" + 
	                "</div>" +
                "</div>";
    $('#loading').append(html);
    $('#loading').css({
        position: 'fix',
        left: '50%',
        top: '50%'
    });
}
// End loading

var menuCustomShowing = false;
var menuBox = null;
// Custom right-click
$('.emp-row-in-list').on('contextmenu', function () {
    menuBox = $('#menu-custom');
    let px = arguments[0].clientX + 'px';
    let py = arguments[0].clientY +'px';
    $('#menu-custom').css({left: px, top: py, display: 'block' });
    arguments[0].preventDefault();
    let empName = '';
    $('legend').html('');
    menuCustomShowing = true;
    return false;
}); 
$(document).on('click', function () {
    if (menuCustomShowing) {
        $('#menu-custom').css({ display: 'none' });
        menuCustomShowing = false;
    }
    return true;
});
// End custom right-click

function GoToBooking(id) {
    window.location.href = "./booking?id=" + id;
}

function Booking(id) {
    window.location.href = "./booknew?bedid=" + id;
}


function ShowingSalariesJustMonthChoose(id)
{
    //alert(id);
    $(".container-fluid>div:not(#" + id + ")").css('display', 'none');
    $("#" + id).css('display', 'block');
    $(".card-header").css('display', 'block');
    if (id === 'all') {
        $(".container-fluid>div:not(#" + id + ")").css('display', 'block');
    }
}
$('#dropdown-choose-year>ul>li').on('click', function () {
    let year = this.children[0].id;
    console.log(year);
    $("#dropdown-month>.dropdown-menu>li>a:not([id*=" + year + "])").css('display', 'none');
    $("#dropdown-month>.dropdown-menu>li>a[id*=" + year + "]").css('display', 'block');
    if (year == 'all-year')
        $("#dropdown-month>.dropdown-menu>li>a").css('display', 'block');
});

function SwitchTo(tab) {
    $('.search-page>ul>li').removeClass('active');
    $(tab).closest('li').addClass('active');
    
    $('.search-page>div:not(#' + tab.id + '-show)').css('display', 'none');
    $('#' + tab.id + '-show').css('display', 'block');
}

$('#btn-search').on('click', function () {
    window.location.href = './search?key=' + $('#txt-search').val();
});

function GoToEmployeePage(index) {
    window.location.href = './employees?index=' + index;
}
function BackEmployeePage() {
    let current = $('#paging-employees>.active').children().find('sub').first().html();
    current -= 1;
    if (current == 0)
        current = 1;
    if (current > 1)
        GoToEmployeePage(1);
}
function NextEmployeePage(totalPages) {
    let current = $('#paging-employees>.active').children().find('sub').first().html();
    current *= 1;
    current += 1;
    if (current >= totalPages)
        current = totalPages;
    if (current < totalPages)
        GoToEmployeePage(totalPages);
}
/* Services */
function GoToServicePage(index) {
    window.location.href = './servicesadmin?page=' + index;
}
function BackServicePage() {
    let current = $('#paging-services>.active').children().find('sub').first().html();
    current -= 1;
    if (current == 0)
        current = 1;
    if (current > 1)
        GoToServicePage(1);
}
function NextServicePage(totalPages) {
    let current = $('#paging-services>.active').children().find('sub').first().html();
    current *= 1;
    current += 1;
    if (current >= totalPages)
        current = totalPages;
    if (current < totalPages)
        GoToServicePage(totalPages);
}
$('#choose-services').on('click', function () {

});

$('#create-serivce-link-to').on('click', function () {
    window.location.href = './createservice'

});
/* End Services */

/* Salary*/
$('#salary-page-all-salaries').on('click', function () {
    $('#table-salary-for-emp tr:not(:first)').remove();
    let url = window.location.href;

    let empId = getParameterByName('empId', url);
    let month = getParameterByName('month', url);
    let year = getParameterByName('year', url);
    let stringHtml = "";
    $.ajax({
        type: 'post',
        //dataType: 'json',
        async: false,
        data: {
            empId: empId,
            month: month,
            year: year
        },
        url: '/admin/GetSalaryForEmployeeInMonth',
        complete: function (xhr) {
        },
        success: function (value) {
            stringHtml = value;
        }
    });
    $('#table-salary-for-emp tr:last').after(stringHtml);
});

$('#update-salary-for-employee').on('click', function () {
    let empId = $('#id-emp').val();
    let salary = $("#Emp_Salary_Salary").val();
    salary = parseFloat(salary);
    $.ajax({
        type: 'post',
        dataType: 'json',
        async: false,
        data: {
            empId: empId,
            salary: salary
        },
        url: '/admin/UpdateSalaryForEmployee',
        complete: function (xhr) {
        },
        success: function (value) {
            if (value == false) {
                let mainAlert = $('#alert-showing-employee-salary').find('#main-alert');

                // Show hide class
                $('#alert-showing-employee-salary').removeClass('hidden');
                mainAlert.removeClass('alert-danger');
                mainAlert.removeClass('alert-default');
                mainAlert.removeClass('alert-inverse');
                mainAlert.removeClass('alert-primary');
                mainAlert.removeClass('alert-warning');
                mainAlert.addClass('alert-warning');

                // Set up text
                mainAlert.find('a:first').html("Can't update salary information");
                mainAlert.find('a:last').html("Please try again");
            }
            else {

                let mainAlert = $('#alert-showing-employee-salary').find('#main-alert');

                // Show hide class
                $('#alert-showing-employee-salary').removeClass('hidden');
                mainAlert.removeClass('alert-danger');
                mainAlert.removeClass('alert-default');
                mainAlert.removeClass('alert-inverse');
                mainAlert.removeClass('alert-primary');
                mainAlert.removeClass('alert-warning');
                mainAlert.addClass('alert-success');


                // Set up text
                mainAlert.find('a:first').html("Success");
                mainAlert.find('a:last').html("Update salary success for employee");
            }
        }
       
    });
});
/* End Salary*/
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$('select').on('change', function () {
    var payId = $(this);
    console.log(payId);
});

let idServicesChoosen = "";
$("input[name*='Services'][type='checkbox']").on('click', function () {
    let id = $(this).siblings('.hidden').val();
    let comma = ',';
    let check = id + comma;
    if (!(idServicesChoosen.indexOf(check) >= 0)) 
        idServicesChoosen += id + ',';
    else
        idServicesChoosen = idServicesChoosen.replace((id + ','), '');
});

$('#book-ok-services').on('click', function () {
    // Handle showing serivices
    $.ajax({
        type: 'post',
        //dataType: 'json',
        async: false,
        data: {
            ids: idServicesChoosen
        },
        url: '/admin/GetCodeForService',
        complete: function (xhr) {
        },
        success: function (value) {
            $('#choose-services').val(value);
        }
    });
    // Handle showing time period
    $.ajax({
        type: 'post',
        //dataType: 'json',
        async: false,
        data: {
            ids: idServicesChoosen
        },
        url: '/admin/GetTimeForService',
        complete: function (xhr) {
        },
        success: function (value) {
            if ($('#time-period').val(value) != "" || $('#time-period').val() != null)
                $('#time-period').closest('div').addClass('is-focused');
        }
    });

});

// Handle create new bed, if success, showing add name for bed in language
$('#create-bed-main-action').on('click', function () {
    let code = $('#BedCode').val();
    $.ajax({
        type: 'post',
        async: false,
        data: {
            code: code
        },
        url: '/admin/PerformCreateBed',
        complete: function (xhr) {
        },
        success: function (value) {
            if (value > 0) $('#bed-created-id').html(value);
            if ($('#div-add-name-in-language').hasClass('hidden'))
                $('#div-add-name-in-language').removeClass('hidden');
        }
    });
});


// Handle create new service, if success, showing add name for service in language
$('#create-service-main-action').on('click', function () {
    let code = $('#ServiceCode').val();
    let hours = $("#hours-cost").val();
    let minutes = $("#minutes-cost").val();
    let cost = $('#cost').val();
    let isActive = $('#is-in-use').is(":checked");
    $.ajax({
        type: 'post',
        async: false,
        data: {
            code: code,
            hours: hours,
            minutes: minutes,
            cost: cost,
            isActive: isActive
        },
        url: '/admin/PerformCreateService',
        complete: function (xhr) {

        },
        success: function (value) {
            if (value > 0) $('#service-created-id').html(value);
            if ($('#div-add-name-in-language').hasClass('hidden'))
                $('#div-add-name-in-language').removeClass('hidden');
        }
    });
});

// Handle add name for bed in language
$(document).on('click', '#btn-add-name-bed', function () {
    let btn = $(this);
    let div = $(this).closest('div .row');
    let bedId = $('#bed-created-id').html();
    let value = div.find('.col-md-7').find('input').val();
    var languageId = div.find('.col-md-3').find('select').val();

    $.ajax({
        type: 'post',
        //dataType: 'json',
        async: false,
        data: {
            bedId: bedId,
            value: value,
            languageId: languageId
        },
        url: '/admin/PerformAddBedNameInLanguage',
        complete: function (xhr) {
            
        },
        success: function (value) {
            if (value === true) {
                if ($('#div-add-name-in-language').hasClass('hidden'))
                    $('#div-add-name-in-language').removeClass('hidden');
                ids.push(languageId);
                let html = PrepareRowAddNameInLanguage('btn-add-name-bed');
                btn.closest('.row').after(html);
                btn.closest('div .col-md-2').remove();
                btn.closest('div .row').find('select').addBack('disabled');
            }
            else {
                alert("Bed has name in this language<br/>Please try in other language");
            }
        }
    });
});

// Handle add name for bed in language
$(document).on('click', '#btn-add-name-service', function () {
    let btn = $(this);
    let div = $(this).closest('div .row');
    let serviceId = $('#service-created-id').html();
    let value = div.find('.col-md-7').find('input').val();
    var languageId = div.find('.col-md-3').find('select').val();

    $.ajax({
        type: 'post',
        //dataType: 'json',
        async: false,
        data: {
            serviceId: serviceId,
            value: value,
            languageId: languageId
        },
        url: '/admin/PerformAddServiceNameInLanguage',
        complete: function (xhr) {
            
        },
        success: function (value) {
            if (value === true) {
                if ($('#div-add-name-in-language').hasClass('hidden'))
                    $('#div-add-name-in-language').removeClass('hidden');
                ids.push(languageId);
                let html = PrepareRowAddNameInLanguage('btn-add-name-service');
                btn.closest('.row').after(html);
                btn.closest('div .col-md-2').remove();
                btn.closest('div .row').find('select').addBack('disabled');
            }
            else {
                alert("Service has name in this language<br/>Please try in other language");
            }
        }
    });
});

let ids = [];
function PrepareRowAddNameInLanguage(namebtn) {
    let options = '';
    $.ajax({
        type: 'post',
        dataType: 'json',
        async: false,
        url: '/admin/GetOthersLanguages',
        data: {
            ids: ids
        },
        complete: function (xhr) {
            console.log("GetAllLanguages complete");
        },
        success: function (rt) {
            let langs = JSON.parse(rt);
            $.each(langs, function (i, item) {
                options += '<option value="' + item.id + '">' + item.value + '</option>';
            });
        }
    });
    if (options != '') {
        let html =  '<div class="row">' + 
                        '<div class="col-md-3">' + 
                            '<div class="form-group label-floating">' + 
                                '<label class="control-label">Language</label>' +
                                '<select class="dropdown form-control" id="Languages" name="Languages">' +
                                + '"' + options + '"' +
                                '</select>' + 
                                '<span class="material-input"></span>' +
                            '</div>' +
                        '</div>' +
                        '<div class="col-md-7">' + 
                            '<div class="form-group label-floating is-empty">' +
                                '<label class="control-label">Value</label>' + 
                                '<input type="text" class="form-control" id="txt-name-for">' + 
                                    '<span class="material-input"></span>' + 
                            '</div>' + 
                        '</div>' + 
                        '<div class="col-md-2">' + 
                            '<div class="form-group">' + 
                                '<input type="button" class="btn btn-primary pull-right" id="' + namebtn + '" value="Add">' + 
                            '</div>' +
                        '</div>' +
                '</div>';
        return html;
    }
}


// Booking New
$('#customer-from-booking').on('change', function () {
    // New customer from booking
    if ($(this).val() === '-1') {

    }

});
// End Booking New 
