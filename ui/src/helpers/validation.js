const validation = {
    /**
     * Thực hiện validate
     * @param {object} rules Luật validate
     * @param {object} data Dữ liệu cần validate
     * @param {object} displayNames Tên hiển thị khi bị lỗi
     * @returns Danh sách lỗi
     */
    validate: (rules, data, displayNames) => {
        var errors = {};
        for (let property in rules) {
            let rs = rules[property].split('|');
            for (let r of rs) {
                let rFunc = (`${r}Property`);
                if (validation[rFunc](data[property])) {
                    errors[property] = validation[`${rFunc}Msg`](displayNames[property]);
                    break;
                }
            }
        }
        return errors;
    },

    /**
     * Kiểm tra required
     * @param {*} value dữ liệu cần kiểm tra
     * @returns true|false
     */
    requiredProperty: (value) => !value ? true : false,

    /**
     * Lời thông báo lỗi required.
     * @param {string} name Tên hiển thị
     * @returns string
     */
    requiredPropertyMsg: name => `${name} không được để trống`
}

export default validation;