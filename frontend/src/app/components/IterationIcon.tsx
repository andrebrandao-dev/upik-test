import { FavouriteIcon, Cancel01Icon } from "hugeicons-react";
import styles from "@/app/styles/IterationIcon.module.css"

const iconMap = {
  favourite: {
    component: FavouriteIcon,
  },
  cancel: {
    component: Cancel01Icon,
  } 
}

interface IterationIconProps {
  icon: IconType,
  onClick?: (icon: string) => void;
}

type IconType = 'favourite' | 'cancel';

const IterationIcon:React.FC<IterationIconProps> = ({icon, onClick}) => {
  const IconComponent = iconMap[icon].component;

  const handleEmitClick = () => {
    if(onClick) {
      onClick(icon);
    }
  }

  return (
    <button
      type="button"
      className={`${styles.icon} ${styles[icon]}`}
      onClick={handleEmitClick}
    >
      { IconComponent ? <IconComponent /> : <span>Icon Not Valid</span> }
    </button>
  )
}

export default IterationIcon;