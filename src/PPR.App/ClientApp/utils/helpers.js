export default class Helpers {
  constructor() {}

  // requests data from server side script using axios-post method

  post(url, data) {
    return axios.post(url, data);
  }

  // requests data from server side script using axios-get method
  get(url, data) {
    return axios.get(url, data);
  }

  all(urls) {
    return axios.all(urls);
  }

  // maps a month into yearly format from a given number

  numericToMonthAsNumber(arr) {
    let months = new Array(12).fill(0);

    arr.forEach((key, val) => {
      months[key.value - 1] = key.count;
    });

    return months;
  }

  // converts a month in numeric format to string format , @return array

  numericToMonthAsString(arr) {
    let months = new Array(12).fill(0);

    arr.forEach((key, val) => {
      key.value = this.monthToNumberMapping(key.value);

      months[key.value - 1] = key.count;
    });

    return months;
  }

  monthToNumberMapping(str) {
    let monthNumber = 0;

    let monthName = str.toLowerCase();

    switch (monthName) {
      case "january":
        monthNumber = 1;
        break;

      case "february":
        monthNumber = 2;
        break;

      case "march":
        monthNumber = 3;
        break;

      case "april":
        monthNumber = 4;
        break;

      case "may":
        monthNumber = 5;
        break;

      case "june":
        monthNumber = 6;
        break;

      case "july":
        monthNumber = 7;
        break;

      case "august":
        monthNumber = 8;
        break;

      case "september":
        monthNumber = 9;
        break;

      case "october":
        monthNumber = 10;
        break;

      case "november":
        monthNumber = 11;
        break;

      case "december":
        monthNumber = 11;
        break;

      default:
        monthNumber = 0;
        break;
    }

    return monthNumber;
  }

  numberToMonthMapping(number) {
    let monthName = "";

    switch (number) {
      case "1":
        monthName = "January";
        break;

      case "2":
        monthName = "February";
        break;

      case "3":
        monthName = "March";
        break;

      case "4":
        monthName = "April";
        break;

      case "5":
        monthName = "May";
        break;

      case "6":
        monthName = "June";
        break;

      case "7":
        monthName = "July";
        break;

      case "8":
        monthName = "August";
        break;

      case "9":
        monthName = "September";
        break;

      case "10":
        monthName = "October";
        break;

      case "11":
        monthName = "November";
        break;

      case "12":
        monthName = "December";
        break;

      default:
        break;
    }

    return monthName;
  }

  // creates array from a given object

  createKeyMapping(params) {
    let data = new Array();

    data["labels"] = [];

    data["data"] = [];

    params.data.forEach(key => {
      data.labels.push(key[params.keyName]);

      data.data.push(key.count);
    });

    return data;
  }

  destroyComponent(component) {
    component = false;
  }

  // starts the specified loader
  initLoader(loaderName) {
    loaderName = true;
  }

  //stops the specified loader
  destroyLoader(loaderName) {
    loaderName = false;
  }

  // parses a given url , splits in the form of given pattern and returns result , can be used as an array.

  // params : (type =>object) { url : url as string or object, pattern : to split the given url string }

  urlParser(params) {
    if (typeof params.url == "object") {
      let url = "";

      Object.keys(params.url).forEach(key => {
        url += `${params.url[key]}|`;
      });

      url = url.slice(0, url.length - 1);

      return url.split(params.pattern);
    }

    return params.url.split(params.pattern);
  }

  // notifications
  notify(settings) {
    switch (settings.type) {
      case "success":
        settings.icon = "fa fa-check";
        settings.duration = 2000;
        break;

      case "warning":
        settings.icon = "fa fa-info-circle";
        settings.duration = 7000;
        break;

      case "danger":
        settings.icon = "fa fa-times";
        settings.duration = 4000;
        break;

      default:
        settings.type = "info";
        settings.icon = "fa fa-info-circle";
        settings.duration = 4000;

        break;
    }

    // console.log(settings.scope.$notify);

    settings.scope.$notify({
      icon: settings.icon,
      type: settings.type,
      title: settings.title,
      content: settings.body,
      placement: settings.placement,
      duration: settings.duration
    });
  }

  // ucs-2 string to base64 encoded ascii
  utoa(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
  }

  // base64 encoded ascii to ucs-2 string
  atou(str) {
    return decodeURIComponent(escape(window.atob(str)));
  }

  // replaces the quotes.
  replaceQuotes(string) {
    if (string !== undefined) {
      return string.replace(/['"]+/g, "");
    }
  }

  /** @@returns limited characters from a string */

  strip(data, len) {
    try {
      if (data == null || data == "") {
        return "N/A";
      }

      data = data.toString();

      return data.length > len ? data.substr(0, len) + " ..." : data;
    } catch (err) {
      console.log(err, "The following data cannot be read :" + data);
    }
  }

  removeExtraWhiteSpaces(value) {
    let replaced = value.replace(/ +/g, " ");
    return replaced;
  }

  convertToLower(value) {
    return value.toLowerCase();
  }

  undefinedCheck(value) {
    return typeof value != "undefined" ? value : "N/A";
  }

  blankSpaceCheck(value) {
    value = this.removeExtraWhiteSpaces(value);
    return value !== "" ? value : "N/A";
  }

  blankSpaceAndNullCheck(value) {
    if (value && value !== null) {
      return this.removeExtraWhiteSpaces(value);
    } else {
      return "N/A";
    }
  }

  bytesToSize(bytes) {
    const sizes = ["Bytes", "KB", "MB", "GB", "TB"];
    if (bytes == 0) return "0 Byte";
    const i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
    return Math.round(bytes / Math.pow(1024, i), 2) + " " + sizes[i];
  }

  getExtensionFromStr(str) {
    let _value;
    if (str != "undefined") _value = str.split(".");
    return _value.length > 1 ? _value[_value.length - 1] : "";
  }

  toPascalCase(string) {
    return `${string}`
      .replace(new RegExp(/[-_]+/, "g"), " ")
      .replace(new RegExp(/[^\w\s]/, "g"), "")
      .replace(
        new RegExp(/\s+(.)(\w+)/, "g"),
        ($1, $2, $3) => `${$2.toUpperCase() + $3.toLowerCase()}`
      )
      .replace(new RegExp(/\s/, "g"), "")
      .replace(new RegExp(/\w/), s => s.toUpperCase());
  }

  now(type) {
    let date = new Date();
    let yyyy = date.getFullYear();
    let dd = date.getDate();
    let mm = date.getMonth() + 1;

    if (dd < 10) gg = "0" + dd;

    if (mm < 10) mm = "0" + mm;

    let hours = date.getHours();
    let minutes = date.getMinutes();
    let seconds = date.getSeconds();

    if (hours < 10) hours = "0" + hours;

    if (minutes < 10) minutes = "0" + minutes;

    if (seconds < 10) seconds = "0" + seconds;

    let time = hours + ":" + minutes + ":" + seconds;

    if (type === 1) {
      return dd + "-" + mm + "-" + yyyy + " " + time;
    } else if (type === 2) {
      return yyyy + "-" + mm + "-" + gg + " " + time;
    }
  }

  htmlEncode(value) {
    if (value) {
      return $("<div/>")
        .text(value)
        .html();
    } else {
      return "";
    }
  }

  fullDateTime(d) {
    var date = new Date(d);
    var n = date.toLocaleString("en-US", {
      hour12: true
    });
    return n;
  }

  getTime(d) {
    var date = new Date(d);
    var n = date.toLocaleString("en-US", {
      hour12: true
    });
    return n.slice(11, 24);
  }
}
