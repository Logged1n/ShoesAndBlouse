import styles from '@/styles/Header/DesktopNavigationBar.module.css';
import Link from "next/link";
import Dropdown from "@/components/Dropdown";

type updateMobileState = () => void;

interface DesktopNavigationBarProps {
    updateMobileState: updateMobileState;
    isMobileActive: boolean;
}

const DesktopNavigationBar = ({
    updateMobileState,
    isMobileActive
}: DesktopNavigationBarProps) => {

    const handleHamburgClick = () => {
        updateMobileState();
    }

    return (
        <nav className={`${styles.primaryNavigation} ${
            isMobileActive ? styles.mobileActive : ''
        }`}
        id={"primary-navigation"}
        >
            <div className={styles.logoContainer}>
                <Link href={"/"}>
                    Shoes&Blouse
                </Link>
            </div>
            <ul role={"list"} className={styles.navList}>
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
                    <Dropdown category={"More"}>
                        <Dropdown category={"Your Account"}/>
                        <Dropdown category={"About Us"}/>
                    </Dropdown>
                </div>
            </ul>
            <div className={styles.hamburgerWrapper} onClick={handleHamburgClick}>
                <span className={styles.upperHamburger} id={"upperHam"}></span>
                <span className={styles.bottomHamburger} id={"bottomHam"}></span>
            </div>

        </nav>
    );
}

export default DesktopNavigationBar;
