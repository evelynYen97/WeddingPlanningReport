//行事曆
$(document).ready(function () {
    // 從 Local Storage 加載事件
    let eventsList = JSON.parse(localStorage.getItem('calendarEvents')) || [];

    // 初始化日曆
    $('#calendar').fullCalendar({
        // 載入 Local Storage 中的事件
        events: eventsList,

        // 允許點擊日期添加事件
        dayClick: function (date, jsEvent, view) {
            $('#dateInput').val(date.format('YYYY-MM-DD')); // 設定日期
            $('#eventInput').val(''); // 清空事件名稱
            $('#eventItemInput').val('請選擇').trigger('change'); // 重置事件類型
            $('#contentInput').val(''); // 清空事件內容

            // 修改 modal 標題名稱
            $('#eventFormModalTitle').text('新增事件');

            // 隱藏刪除按鈕，因為是新增事件
            $('#eventFormDelButton').hide();

            // 顯示 modal
            $('#eventFormModal').modal('show');
        },

        // 允許點擊事件編輯
        eventClick: function (calEvent, jsEvent, view) {
            $('#dateInput').val(calEvent.start.format('YYYY-MM-DD')); // 填入事件日期
            $('#eventInput').val(calEvent.title); // 填入事件名稱
            $('#eventItemInput').val(calEvent.eventItem).trigger('change'); // 填入事件類型
            $('#contentInput').val(calEvent.description); // 填入事件內容

            // 修改 modal 標題名稱
            $('#eventFormModalTitle').text('編輯事件');

            // 顯示刪除按鈕，因為是編輯事件
            $('#eventFormDelButton').show();

            // 顯示 modal
            $('#eventFormModal').modal('show');

            // 刪除按鈕事件
            $('#eventFormDelButton').off('click').on('click', function () {
                // 刪除日曆中的事件
                console.log("Deleting Event ID:", calEvent._id);

                if (confirm('確定要刪除這個事件嗎？')) {
                    // 找到事件的索引
                    const eventIndex = eventsList.findIndex(event => event._id === Number(calEvent._id));

                    if (eventIndex !== -1) {
                        // 刪除事件
                        eventsList.splice(eventIndex, 1);

                        // 更新 Local Storage
                        localStorage.setItem('calendarEvents', JSON.stringify(eventsList));

                        // 刪除日曆中的事件
                        $('#calendar').fullCalendar('removeEvents', calEvent._id);

                        // 隱藏 modal
                        $('#eventFormModal').modal('hide');
                    } else {
                        alert("未找到事件，無法刪除。");
                    }
                }
            });
        }
    });

    // 點擊「確定」按鈕時保存事件
    $('#eventFormOkButton').click(function () {
        var eventTitle = $('#eventInput').val(); // 事件名稱
        var eventDate = $('#dateInput').val();   // 事件日期
        var eventItem = $('#eventItemInput').val(); // 事件類型
        var eventContent = $('#contentInput').val(); // 事件內容

        // 檢查是否輸入了有效的事件名稱
        if (eventTitle) {
            // 新增事件
            var newEvent = {
                _id: new Date().getTime(), // 使用時間戳作為唯一 ID
                title: eventTitle,
                start: eventDate,
                description: eventContent,
                eventItem: eventItem
            };

            // 保存事件到 Local Storage
            eventsList.push(newEvent);
            localStorage.setItem('calendarEvents', JSON.stringify(eventsList));

            // 更新日曆事件
            $('#calendar').fullCalendar('renderEvent', newEvent, true); // 即時渲染事件

            // 隱藏 modal
            $('#eventFormModal').modal('hide');
        } else {
            alert("請輸入事件名稱");
        }
    });

    // 點擊「取消」按鈕或關閉按鈕時關閉模態框
    $('#cancelButton, .close').click(function () {
        $('#eventFormModal').modal('hide');
    });
});