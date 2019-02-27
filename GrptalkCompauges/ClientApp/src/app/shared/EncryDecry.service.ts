import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';



@Injectable()


export class EncryDecry {

  constructor() { }

  //The set method is use for encrypt the value.
  set(keys, value) {
    //var key = CryptoJS.enc.Utf8.parse(keys);
    //var iv = CryptoJS.enc.Utf8.parse(keys);
    var encrypted = CryptoJS.AES.encrypt(value.toString(), keys);

    return encrypted.toString();
  }

  //The get method is use for decrypt the value.
  get(keys, value) {
    var decrypedText = "";

    var bSuccess = false;
        var decrypted = CryptoJS.AES.decrypt(value, keys);
        decrypedText = decrypted.toString(CryptoJS.enc.Utf8);
    return decrypedText;

  }

  // To Get Sha256 Encryption code
  sha3Encryption(value)
    {
      var encryptsha = CryptoJS.SHA3(value);
    return encryptsha.toString();
    }
}
