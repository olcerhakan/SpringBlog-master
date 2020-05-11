
function slugFunc() {

    this.ajaxUrl = null;  //verilmesi gerekiyor. açık açık "/Slug/ConvertToSlug" yazmadık. başına ~ ile view üzerine yazıyoruz ki oraya başka dosya gelebilir. Proje patlayabilir. Bu şekilde statik değer kullanmıyoruz.Ürettiriyoruz dışarıdan.

    // birtane init diye func oluşturdum. başlat manasında. ajaxUrl yi vererek hazır hale getirebilir.
    this.init = function (ajaxUrl) {
        this.ajaxUrl = ajaxUrl; //initle girdi
    };


    // textboxtan cıkınca catname dan cıkınca değişmesini sağlıyordu. eventler kullandık. change ve click
    this.run = function (sourceSelector, targetSelector, refreshSelector) {

        //func çalışırken local nesneye bu nesneyi kullandım
        var _ajaxUrl = this.ajaxUrl;

        //burdaki ajaxUrl sine bu nesneyi gönder dedik. Bu generate slag event içinde cağırıldığı için bu func sahıbı event. event içinde tetiklenmeli.

        function generateSlug() {
            console.log(_ajaxUrl);
            var title = $(sourceSelector).val();
            if (!title) return;

            $.post(_ajaxUrl, { title: title }, function (data) {
                $(targetSelector).val(data);
                $(targetSelector).trigger("blur"); // in order to trigger validation
            });
        }

        $(sourceSelector).change(function () {
            if (!$(targetSelector).val()) {
                generateSlug();
            }
        });

        $(refreshSelector).click(function (event) {
            event.preventDefault();
            generateSlug();
        });

    };
}

var slug = new slugFunc();