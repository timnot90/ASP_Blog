
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Error404</title>
    <link href="/Content/site.css" rel="stylesheet" />
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <script src="/Scripts/modernizr-2.7.2.js"></script>

    <script src="/Scripts/jquery-2.1.0.js"></script>
    <script src="/Scripts/modern-business.js"></script>
    <script src="/Scripts/bootstrap.js"></script>

</head>
    <%
        Response.Status = "404 Not Found"
        Response.ContentType = "text/html"
        Response.Expires = 0
        Response.CacheControl = "no-cache"
        Response.AddHeader "Pragma", "no-cache"
    %>
<body>
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <!-- You'll want to use a responsive image option so this logo looks good on devices - I recommend using something like retina.js (do a quick Google search for it and you'll find it) -->
                <a class="navbar-brand" href="/">Timo&#39;s Blog</a>
            </div>

            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav navbar-right">
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="glyphicon glyphicon-list"></i> Blog <b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="/">
                                    <i class="glyphicon glyphicon-list"></i> View Posts
                                </a>
                            </li>
                            <li>
                                <a href="/Home/Categories">
                                    <i class="glyphicon glyphicon-inbox"></i> Categories
                                </a>
                            </li>
                            <li>
                                <a href="/Home/AddBlogentry">
                                    <i class="glyphicon glyphicon-plus-sign"></i> Add Entry
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                            <i class="glyphicon glyphicon-user"></i>
                            My Profile
                            <b class="caret"></b>

                        </a>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="/Account/Register">
                                    <i class="glyphicon glyphicon-edit"></i> Register
                                </a>
                            </li>
                            <li>
                                <a href="/Account/Login">
                                    <i class="glyphicon glyphicon-log-in"></i> Login
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a href="/About">
                            <i class="glyphicon glyphicon-info-sign"></i> About
                        </a>
                    </li>
                    <li>
                    </li>
                    <li>
                        <a href="/Account/Login">
                            <i class="glyphicon glyphicon-log-in"></i> Login
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">
                    Error404
                    <small>Timo's Blog</small>
                </h1>
                <ol class="breadcrumb">
                    <li>
                        Error404
                    </li>
                </ol>
            </div>
        </div>
    </div>
    <div class="container">










        <h2>An error occurred while processing your request. The requested page wasn't found</h2>







    </div>
    <div class="container panel-footer">
        <hr>
        <footer>
            <div class="row">
                <div class="col-lg-12">
                    <p>Copyright &copy; Company 2013</p>
                </div>
            </div>
        </footer>
    </div>


    <!-- Visual Studio Browser Link -->
    <script type="application/json" id="__browserLink_initializationData">
        {"appName":"Chrome","requestId":"c492cf5640fb4baa950030ded53455a6"}
    </script>
    <script type="text/javascript" src="http://localhost:50628/6d030b5672c84d7cafa513545b88ca1c/browserLink" async="async"></script>
    <!-- End Browser Link -->

</body>

</html>