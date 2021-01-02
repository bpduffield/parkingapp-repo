$(document).ready(

    function ()
    {
        $('#departmentGroup').hide();
        $('#organizationName').hide();
        $('#userRole').change(

            function ()
            {
                var userRole = $('#userRole').val();

                if ((userRole == 'WVUEmployee') || (userRole == 'ParkingEmployee'))
                {
                    $('#departmentGroup').show();
                    $('#organizationName').hide();
                    
                }
                else if (userRole == 'Visitor')
                {
                    $('#organizationName').show();
                    $('#departmentGroup').hide();

                }
                else
                {
                    $('#organizationName').hide();
                    $('#departmentGroup').hide();
                }
            }
            

        );



    }

);