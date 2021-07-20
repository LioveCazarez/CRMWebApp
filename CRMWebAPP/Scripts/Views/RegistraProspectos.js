var Restablece = {};

(function (self) {
    "use strict";
    var formID = "RegPros";
    var form = function (selector) { return document.getElementById(selector) }
    var formClass = function (selector) { return document.getElementsByClassName(selector) }

    $(document).ready(function (e) {
    });

    var ctrls = {
        get btnSave() { return form("EnviaDatos"); },
    }

    var urls = {
        get RegistraProspecto() { return "/RegistroProspecto/InsertaProspecto" },
        get InsertaArchivo() { return "/RegistroProspecto/InsertaArchivo" },
    }

    var event = {
        RegistraProspecto: function (e) {



            var Nombre = $('#nombre').val();
            var ApellidoP = $('#apellidoP').val();
            var ApellidoM = $('#apellidoM').val();
            var Calle = $('#Calle').val();
            var Numero = $('#Numero').val();
            var Colonia = $('#Colonia').val();
            var CodigoP = $('#codigoPostal').val();
            var Telefono = $('#telefono').val();
            var rfc = $('#rfc').val();



            if ((!Nombre == '') && (!ApellidoP == '') && (!Calle == '') && (!Numero == '') && (!Colonia == '') && (!CodigoP == '') && (!Telefono == '') && (!rfc == '')) {
                var data = JSON.stringify({
                    'Nombre': Nombre, 'Apellido_Paterno': ApellidoP, 'Apellido_Materno': ApellidoM, 'Calle': Calle, 'Numero': Numero,
                    'Colonia': Colonia, 'Codigo_Postal': CodigoP, 'Telefono': Telefono, 'RFC': rfc,
                });

                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    type: 'POST',
                    url: urls.RegistraProspecto,
                    data: data,
                    success: function (data) {

                        $('#divFormulario').hide();
                        $('#divMensaje').show();



                        if (data.error) {
                            alert(data.Mensaje);
                            return;
                        }
                    },
                    error: function (data) {
                    }
                });


                $('#RegPros').hide();
                $('#divMensaje').show();
                alert('Se ha registrado el prospecto ' + Nombre + ' exitosamente');
                
                readBlob();
               
            } else {
                alert('Es necesario llenar todos los campos')
            }        


        },

    }

    var eventos = function () {
        ctrls.btnSave.onclick = event.RegistraProspecto
    }

   

    //--------------------

    
    function readBlob() {

        
        var rfc = $('#rfc').val();
        var files = document.getElementById('files').files;
        

        var file = files[0];
        var blob = file.slice();

        var filetype = file.type;
        var filename = file.name;

        var reader = new FileReader();

        reader.onloadend = function (evt) {
            if (evt.target.readyState == FileReader.DONE) { 

                var cont = evt.target.result
                var base64String = getB64Str(cont);

                var model = {
                    'archivos': base64String,
                    'rfc': rfc,
                    'nombreArchivo': filename
                };

                $.ajax({
                    url: urls.InsertaArchivo,
                    type: 'POST',
                    data: JSON.stringify(model),
                    processData: false,
                    async: false,
                    contentType: 'application/json; charset=utf-8',
                    complete: function (data) {
                        
                    },
                    error: function (response) {
                       
                    }
                });
            }
        };

        reader.readAsArrayBuffer(blob);
    }

    function getB64Str(buffer) {
        var binary = '';
        var bytes = new Uint8Array(buffer);
        var len = bytes.byteLength;
        for (var i = 0; i <= len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }
        return binary;
    }
    
    //-------------------------
   

    function inicializaControles(e) {
        if (e) e.preventDefault();
    }

    var init = function () {
        inicializaControles();
        eventos();
    }

    $(init)
})(Restablece);



