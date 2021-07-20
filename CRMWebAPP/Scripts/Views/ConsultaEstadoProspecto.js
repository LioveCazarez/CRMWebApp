var consulta = {};

(function (self) {

    var form = function (selector) { return document.getElementById(selector) }
    var formClass = function (selector) { return document.getElementsByClassName(selector) }
    var usuario = '';  
    var rfcs = '';
    

        $(document).ready(function (e) {
        });
     
        var ctrls = {
            get editar() { return form("editar"); },
        }

        var urls = {
            get ModificaUsuario() { return "/ConsultaEstadoProspecto/ModificaUsuario" },
        }


    $('.btnEditar').click(function () {
        var valores = "";
        $(this).parents("tr").find(".usuario").each(function () {
            valores = $(this).text().replace(/ /g, "");
            valores = valores.replace(/\r?\n|\r/g, "");
        });        
        usuario = valores;
        $('#mensaje').html('¿Esta seguro de modificar el usuario ' + usuario + '?');
         $('#modal').modal();
    });


    $('.btnEditar').click(function () {
        var rfc = "";
        $(this).parents("tr").find(".rfc").each(function () {
            rfc = $(this).text().replace(/ /g, "");
            rfc = rfc.replace(/\r?\n|\r/g, "");
        });
        rfcs = rfc;      
    });
    

    var event = {
        
        Modifica: function (e) {
            var estado = $('#estado option:selected').text();
            var comentarios = $('#comentarios').val();
            
            var data = JSON.stringify({
                'nombre': rfcs, 'estatus': estado, 'comentarios': comentarios,

                });

            $.ajax({
                
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                url: urls.ModificaUsuario,
                data: data,
                success: function (data) {
                    if (data.success) {
                        $('#modal').modal('toggle');                         
                        location.reload();                       
                                      
                    }
                    else {
                        $('#modal').modal('toggle');
                        return;
                    }

                    if (data.error) {
                        $('#modal').modal('toggle');
                        return;
                    }
                   
                },
                error: function (data) {
                }
               
            });

            },
        }


        var eventos = function () {
            ctrls.editar.onclick = event.Modifica
        }

        function inicializaControles(e) {
            if (e) e.preventDefault();
        }

        var init = function () {
            inicializaControles();
            eventos();
        }

        $(init)

})(consulta);