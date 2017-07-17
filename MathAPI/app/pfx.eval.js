/**
 * Created by Nikitas on 7/16/2017.
 */

var PfxCalculator = (function () {
    var vars = [];
    function valueit(x, v, u) {
        if (x === vars[0] || x === vars[1]) {
            if (x === "x") return v;
            else
                return u;
        }
        else return parseFloat(x);
    }

    function isOperand(x) {
        var is = false;
        is = !isNaN(x) || (x === vars[0] || x === vars[1]);
        return is;
    }

    this.valueAt = function (pf, v, u) {
        var stack = Array();
        var top = 0;
        var res = 0.0;
        var i = 0;
        var x = "";
        var a = 0.0;
        var b = 0.0;
        while (true) {
            x = pf[i];
            i++;
            if (isOperand(x)) {
                stack[top] = valueit(x, v, u);
                top++;
            } else if (x === "s") {
                if (top - 1 == 0) return stack[top - 1]; else return null;
            }
            else {
                var j = top - 1;
                if (j < 0) return null; else
                    switch (x) {
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                        case "^":
                            if (j - 1 < 0) return null;
                            b = stack[j];
                            a = stack[j - 1];
                            top = j - 1;
                            switch (x) {
                                case "+":
                                    res = a + b;
                                    break;
                                case "-":
                                    res = a - b;
                                    break;
                                case "*":
                                    res = a * b;
                                    break;
                                case "/":
                                    res = a / b;
                                    break;
                                case "^":
                                    res = Math.pow(a, b);
                                    break;
                            }
                            break;
                        case 'sin':
                        case 'cos':
                        case 'tan':
                        case 'cot':
                        case 'exp':
                        case 'log':
                        case 'atan':
                        case "ln":
                            if (j < 0) return null;
                            a = stack[j];
                            top = j;
                            switch (x) {
                                case "sin":
                                    res = Math.sin(a);
                                    break;
                                case "cos":
                                    res = Math.cos(a);
                                    break;
                                case "tan":
                                    res = Math.tan(a);
                                    break;
                                case "cot":
                                    res = 1 / Math.tan(a);
                                    break;
                                case "atan":
                                    res = Math.atan(a);
                                    break;
                                case "exp":
                                    res = Math.exp(a);
                                    break;
                                case "ln":
                                    res = Math.log(a);
                                    break;
                                case "log":
                                    res = Math.log(a) / Math.log(10);
                                    break;
                            }
                            break;
                    }
                stack[top] = res;
                top++;
            }
        }
    };
    var setVars = function (v, u) { vars = [v, u] };
    return {
        variables: setVars,
        Eval: valueAt
    }

}(PfxCalculator || {}));