


//是否为非法字符
function illegalCharacter(str) {

    var patrn = /[`~!@#$%^&*()_+<>?:"{},.\/;'[\]]/im;

    if (patrn.test(str)) {
        return false;
    }
    return true;
}

//身份证号验证  , thiss:this
function VerifyIdentityCard(thiss) {
    if (thiss.innerHTML.length != 18) {
        thiss.innerHTML = thiss.innerHTML.slice(0, 18);
        alert('输入的身份证位号不对')
        return;
    };
    console.log(thiss.innerHTML.toUpperCase().endsWith("X"));
    console.log(thiss.innerHTML.match(/\D/g));
    if (thiss.innerHTML.match(/\D/g) != null && (thiss.innerHTML.match(/\D/g).length > 1 || !thiss.innerHTML.toUpperCase().endsWith("X"))) {
        alert('号码中含有非数字字符')
        thiss.innerHTML = thiss.innerHTML.replace(/\D/g, '')
    }
}

//11位手机号验证  , thiss:this
function VerifyPhone(thiss) {
    console.log(thiss);
    if (thiss.innerHTML.length > 11) {
        thiss.innerHTML = thiss.innerHTML.slice(0, 11);
        alert('输入的手机号大于11位')
    };
    if (thiss.innerHTML.match(/\D/g) != null) {
        alert('号码中含有非数字字符')
    }
    thiss.innerHTML = thiss.innerHTML.replace(/\D/g, '')
}