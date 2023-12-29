function Validator(options) {
    const getParent = (element, selector) => {
        while (element.parentElement) {
            if (element.parentElement.matches(selector)) {
                return element.parentElement;
            }
            element = element.parentElement;
        }
    };

    const selectorRules = {};

    const validate = (inputElement, rule) => {
        const errorElement = getParent(inputElement, options.formGroupSelector).querySelector(options.errorSelector);
        let errorMessage;

        const rules = selectorRules[rule.selector];

        for (const ruleFunction of rules) {
            switch (inputElement.type) {
                case "radio":
                case "checkbox":
                    errorMessage = ruleFunction(formElement.querySelector(`${rule.selector}:checked`));
                    break;
                default:
                    errorMessage = ruleFunction(inputElement.value);
            }
            if (errorMessage) break;
        }

        if (errorMessage) {
            errorElement.innerText = errorMessage;
            getParent(inputElement, options.formGroupSelector).classList.add("invalid");
        } else {
            errorElement.innerText = "";
            getParent(inputElement, options.formGroupSelector).classList.remove("invalid");
        }

        return !errorMessage;
    };

    const formElement = document.querySelector(options.form);

    if (formElement) {
        formElement.onsubmit = (e) => {
            e.preventDefault();

            let isFormValid = true;

            options.rules.forEach((rule) => {
                const inputElement = formElement.querySelector(rule.selector);
                const isValid = validate(inputElement, rule);
                if (!isValid) {
                    isFormValid = false;
                }
            });

            if (isFormValid) {
                if (typeof options.onSubmit === "function") {
                    const enableInputs = formElement.querySelectorAll("[name]");
                    const formValues = Array.from(enableInputs).reduce((values, input) => {
                        switch (input.type) {
                            case "radio":
                                values[input.name] = formElement.querySelector(`input[name="${input.name}"]:checked`).value;
                                break;
                            case "checkbox":
                                if (!input.matches(":checked")) {
                                    values[input.name] = "";
                                    return values;
                                }
                                if (!Array.isArray(values[input.name])) {
                                    values[input.name] = [];
                                }
                                values[input.name].push(input.value);
                                break;
                            case "file":
                                values[input.name] = input.files;
                                break;
                            default:
                                values[input.name] = input.value;
                        }

                        return values;
                    }, {});
                    options.onSubmit(formValues);
                } else {
                    formElement.submit();
                }
            }
        };

        options.rules.forEach((rule) => {
            if (Array.isArray(selectorRules[rule.selector])) {
                selectorRules[rule.selector].push(rule.test);
            } else {
                selectorRules[rule.selector] = [rule.test];
            }

            const inputElements = formElement.querySelectorAll(rule.selector);

            Array.from(inputElements).forEach((inputElement) => {
                inputElement.onblur = () => validate(inputElement, rule);

                inputElement.oninput = () => {
                    const errorElement = getParent(inputElement, options.formGroupSelector).querySelector(options.errorSelector);
                    errorElement.innerText = "";
                    getParent(inputElement, options.formGroupSelector).classList.remove("invalid");
                };
            });
        });
    }
}

Validator.isRequired = function (selector, message) {
    return {
        selector,
        test: function (value) {
            return value ? undefined : message || "Vui lòng nhập trường này";
        },
    };
};

Validator.isEmail = function (selector, message) {
    return {
        selector,
        test: function (value) {
            const regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            return regex.test(value) ? undefined : message || "Trường này phải là email";
        },
    };
};

Validator.minLength = function (selector, min, message) {
    return {
        selector,
        test: function (value) {
            return value.length >= min ? undefined : message || `Vui lòng nhập tối thiểu ${min} kí tự`;
        },
    };
};

Validator.isConfirmed = function (selector, getConfirmValue, message) {
    return {
        selector,
        test: function (value) {
            return value === getConfirmValue() ? undefined : message || "Giá trị nhập vào không chính xác";
        },
    };
};
function changeOverlay() {
    const buttonClickChange = document.querySelector(".button-submit-overlay");
    const overlayChange = document.querySelector(".overlay-form");
    const formWrapper = document.querySelector(".form-wrapper");

    let changeText = overlayChange.classList.contains("change-side");

    overlayChange.classList.toggle("change-side");
    formWrapper.classList.toggle("right-panel-active");
    buttonClickChange.innerHTML = changeText ? "Register" : "Log In";
}

