function drag_start(event) {
            var style = window.getComputedStyle(event.target, null);
            event.dataTransfer.setData("text/html",
            (parseInt(style.getPropertyValue("left"), 10) - event.clientX) + ',' + (parseInt(style.getPropertyValue("top"), 10) - event.clientY));
        }
        function drag_over(event) {
            event.preventDefault();

            return false;
        }
        function drop(event) {
            event.stopPropagation();
            var offset = event.dataTransfer.getData("text/html").split(',');
            var dm = document.getElementById('dral');

            dm.style.left = (event.clientX + parseInt(offset[0], 10)) + 'px';
            dm.style.top = (event.clientY + parseInt(offset[1], 10)) + 'px';

            event.preventDefault();

            return false;
        }
        var dm = document.getElementById('dral');
        dm.addEventListener('dragstart', drag_start, false);
        document.body.addEventListener('dragover', drag_over, false);
        document.body.addEventListener('drop', drop, false);