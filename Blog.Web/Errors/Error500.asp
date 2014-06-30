<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Error500</title>
    <link href="/Content/site.css" rel="stylesheet" />
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <script src="/Scripts/modernizr-2.7.2.js"></script>

    <script src="/Scripts/jquery-2.1.0.js"></script>
    <script src="/Scripts/modern-business.js"></script>
    <script src="/Scripts/bootstrap.js"></script>

</head>
<%
        Response.Status = "500 Internal Server Error"
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
                <a class="navbar-brand" href="/" rel="noreferrer">Timo's Blog</a>
            </div>

            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav navbar-right">
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" rel="noreferrer"><i class="glyphicon glyphicon-list"></i>Blog <b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="/" rel="noreferrer">
                                    <i class="glyphicon glyphicon-list"></i>View Posts
                                </a>
                            </li>
                            <li>
                                <a href="/Home/Categories" rel="noreferrer">
                                    <i class="glyphicon glyphicon-inbox"></i>Categories
                                </a>
                            </li>
                            <li>
                                <a href="/Home/AddBlogentry" rel="noreferrer">
                                    <i class="glyphicon glyphicon-plus-sign"></i>Add Entry
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" rel="noreferrer">
                            <i class="glyphicon glyphicon-user"></i>
                            timo
                            <b class="caret"></b>

                        </a>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="/Account/EditProfile" rel="noreferrer">
                                    <i class="glyphicon glyphicon-cog"></i>Edit Profile
                                </a>
                            </li>
                            <li>
                                <a href="/Account/Logout" rel="noreferrer">
                                    <i class="glyphicon glyphicon-log-out"></i>Logout
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a href="/About" rel="noreferrer">
                            <i class="glyphicon glyphicon-info-sign"></i>About
                        </a>
                    </li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" rel="noreferrer"><i class="glyphicon glyphicon-signal"></i>Administration <b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="/Administration/Administration/Users" rel="noreferrer">
                                    <i class="glyphicon glyphicon-eye-open"></i>Users
                                </a>
                            </li>
                            <li>
                                <a href="/Administration/Administration/BlogSettings" rel="noreferrer">
                                    <i class="glyphicon glyphicon-eye-open"></i>Settings
                                </a>
                            </li>
                            <li></li>
                        </ul>
                    </li>
                    <li>
                        <a href="/Account/Logout" rel="noreferrer">
                            <i class="glyphicon glyphicon-log-out"></i>Logout
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <br>
    <br>
    <div class="container">

        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">Error 500
            <small>Timo's Blog</small>
                </h1>
            </div>
        </div>
    </div>
    <div class="container">


        <div class="row">
            <div class="col-lg-12">

                <h2>An error occurred while processing your request.</h2>
            </div>
        </div>
    </div>
    <div class="container">

        <div class="row">
            <div class="col-lg-12">
                <hr>
                <footer>
                    <div class="row">
                        <div class="col-lg-12">
                            <p>Your Footer Text. Go to the settings to change it.</p>
                        </div>
                    </div>
                </footer>

            </div>
        </div>

    </div>


    <!-- Visual Studio Browser Link -->
    <script type="application/json" id="__browserLink_initializationData">
    {"appName":"Chrome","requestId":"4d8abeec63db4baf9da9a8583358dbc9"}
    </script>
    <script type="text/javascript" src="http://localhost:55135/76bfe348acb64255b32670d239193260/browserLink" async="async"></script>
    <!-- End Browser Link -->



</body>

</html>
