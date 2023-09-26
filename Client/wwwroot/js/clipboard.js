window.clipboardCopy = {
    copyText: function (codeElement) {
        navigator.clipboard.writeText(codeElement).then(function () {
            //alert("Copied to clipboard!");
        })
            .catch(function (error) {
                //alert(error);
            });
    }
}