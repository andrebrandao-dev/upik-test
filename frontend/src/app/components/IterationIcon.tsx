import { FavouriteIcon, Cancel01Icon } from "hugeicons-react";
import styles from "@/app/styles/IterationIcon.module.css"
import { useState } from "react";

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
  onClick?: (liked: boolean) => void;
}

type IconType = 'favourite' | 'cancel';

const IterationIcon:React.FC<IterationIconProps> = ({icon, onClick}) => {
  const [hover, setHover] = useState<boolean>(false);

  const IconComponent = iconMap[icon].component;

  const handleEmitClick = () => {
    if(onClick) {
      onClick(icon === 'favourite');
    }
  }

  return (
    <div className="relative">
      {
        hover && (
          <div className={ styles.tooltip }>
            <span className="text-slate-200 font-bold text-[10px]">
              { icon === 'favourite' ? 'Amei!' : 'Não Amei' }
            </span>
          </div>
        )
      }
      <button
        type="button"
        className={`${styles.icon} ${styles[icon]}`}
        onClick={handleEmitClick}
        onMouseEnter={() => setHover(true)}
        onMouseLeave={() => setHover(false)}
      >
        { IconComponent ? <IconComponent /> : <span>Icon Not Valid</span> }
      </button>
    </div>
  )
}

export default IterationIcon;