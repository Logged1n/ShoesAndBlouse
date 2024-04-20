import styles from "@/styles/page.module.css";
import Dropdown from "../components/Dropdown";
import Image from 'next/image';
import homepage from ".//../../public/homepage.jpg";
import {Dancing_Script} from 'next/font/google';

const font = Dancing_Script({
    weight: '700',
    subsets: ['latin']
})
export default function Home() {
    return (
        <div className={"container"}>
            <div className={styles.dropdown}>
                <Dropdown category={"New"}>
                    <Dropdown category={"Shoes"}/>
                    <Dropdown category={"Clothes"}/>
                    <Dropdown category={"Accessories"}/>
                </Dropdown>
                <Dropdown category={"Men"}>
                    <Dropdown category={"Shoes"}/>
                    <Dropdown category={"Clothes"}/>
                    <Dropdown category={"Accessories"}/>
                </Dropdown>
                <Dropdown category={"Women"}>
                    <Dropdown category={"Shoes"}/>
                    <Dropdown category={"Clothes"}/>
                    <Dropdown category={"Accessories"}/>
                </Dropdown>
                <Dropdown category={"Kids"}>
                    <Dropdown category={"Shoes"}/>
                    <Dropdown category={"Clothes"}/>
                    <Dropdown category={"Accessories"}/>
                </Dropdown>
                <Dropdown category={"About Us"}/>
                <Dropdown category={"More"}/>
            </div>
            <div style={{ display: 'flex'}}>
                <div>
                    <Image
                        src={homepage}
                        alt="Home Page Pic"
                        width={600}
                        height={600}
                    />
                </div>
                <div className={styles.text}>
                    <h1 className={font.className}> Welcome to our store!</h1>
                    <br></br>
                    <p> Welcome to Shoes and Blouse!</p>
                    <br></br>
                    <p> Your One and Only place for fashion shopping!</p>
                </div>
            </div>
        </div>
    );
}

