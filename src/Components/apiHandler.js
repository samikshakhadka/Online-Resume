import axios from "axios";
function encrypt() {
  return "encryptedToken";
}
const AxiosClient = () => {
  var enc = encrypt();
  return axios.create({
    baseURL: "https://localhost:7222/api/",
    headers: {
      Authorization: enc,
      "Content-Type": "application/json",
      Accept: "application/json",
    },
  });
};
export function readRequest(slug) {
  return AxiosClient().get(slug);
}