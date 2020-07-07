(function () {
  function init() {
    // document
    //   .getElementById("registrationForm")
    //   .addEventListener("submit", testForm);
  }

  function testForm(event) {
    var userResult = testUsername(event.target[0].value);
    var passResult = testPassword(event.target[4].value);

    var result = userResult && passResult;

    if (!result) {
      event.preventDefault();
    }
  }

  function testUsername(userName) {
    return userName.length > 5;
  }

  function testPassword(password) {
    var length = false;
    var special = false;
    var specialChars = "!@#$%^&*()";

    length = password.length >= 10;

    specialChars.split("").forEach(c => {
      if (password.indexOf(c) > -1) {
        special = true;
      }
    });

    return length && special;
  }

  init();
})();
