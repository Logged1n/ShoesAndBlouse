import styles from "@/styles/page.module.css";
import Dropdown from "../components/Dropdown";

export default function Home() {
  return (
    <div className={"container"}>
        <h1>Home Page</h1>
        <h2>It's and h2 tag</h2>
        <h3>And that's h3</h3>
        <p>Super interesting placeholder text in pragraph.</p>
        <Dropdown category={"Shoes"}>
            <Dropdown category={"Blouse"}/>
        </Dropdown>
    </div>
  );}

